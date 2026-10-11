using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Reel : MonoBehaviour
{
    [Header("Symbols: Fixed Order")]
    [SerializeField] private SymbolData[] symbols;

    [Header("Three Reel Images")]
    [SerializeField] private Image topImage;
    [SerializeField] private Image middleImage;
    [SerializeField] private Image bottomImage;

    [Header("Spin Settings")]
    [SerializeField] private float spinDuration = 2.5f;
    [SerializeField] private float fastStepDuration = 0.04f;
    [SerializeField] private float slowStepDuration = 0.22f;
    [SerializeField] private int minimumExtraSteps = 5;

    private RectTransform topRect;
    private RectTransform middleRect;
    private RectTransform bottomRect;

    private Vector2 topStart;
    private Vector2 middleStart;
    private Vector2 bottomStart;

    private float symbolHeight;
    private int middleIndex;
    private bool isSpinning;

    public bool IsSpinning => isSpinning;

    public SlotSymbol CurrentSymbol =>
        symbols[middleIndex].symbol;

    private void Awake()
    {
        if (symbols == null || symbols.Length != 4)
        {
            Debug.LogError(
                "Assign exactly 4 symbols in the fixed order.", this);
            enabled = false;
            return;
        }

        if (topImage == null || middleImage == null ||
            bottomImage == null)
        {
            Debug.LogError("Assign all three reel Images.", this);
            enabled = false;
            return;
        }

        topRect = topImage.rectTransform;
        middleRect = middleImage.rectTransform;
        bottomRect = bottomImage.rectTransform;

        topStart = topRect.anchoredPosition;
        middleStart = middleRect.anchoredPosition;
        bottomStart = bottomRect.anchoredPosition;

        symbolHeight = middleRect.rect.height;

        if (symbolHeight <= 0f)
        {
            Debug.LogError("Middle Image must have a positive height.", this);
            enabled = false;
            return;
        }

        // Image centers should be exactly one symbol height apart.
        if (Mathf.Abs(topStart.y - middleStart.y - symbolHeight) > 1f ||
            Mathf.Abs(middleStart.y - bottomStart.y - symbolHeight) > 1f)
        {
            Debug.LogWarning(
                "Position the Images exactly one symbol height apart.", this);
        }

        middleIndex = Random.Range(0, symbols.Length);
        SetVisibleSymbols();
    }

    private int WrapIndex(int index)
    {
        return (index % symbols.Length + symbols.Length)
               % symbols.Length;
    }

    private void SetVisibleSymbols()
    {
        // Fixed strip order: Seven, Cherry, Bell, Bar.
        // Neighbors are taken from the same repeating sequence.
        topImage.sprite = symbols[WrapIndex(middleIndex - 1)].sprite;
        middleImage.sprite = symbols[middleIndex].sprite;
        bottomImage.sprite = symbols[WrapIndex(middleIndex + 1)].sprite;
    }

    private void SetOffset(float offset)
    {
        topRect.anchoredPosition =
            topStart + Vector2.down * offset;

        middleRect.anchoredPosition =
            middleStart + Vector2.down * offset;

        bottomRect.anchoredPosition =
            bottomStart + Vector2.down * offset;
    }

    private void ResetPositions()
    {
        topRect.anchoredPosition = topStart;
        middleRect.anchoredPosition = middleStart;
        bottomRect.anchoredPosition = bottomStart;
    }

    public IEnumerator Spin(int targetIndex)
    {
        if (isSpinning)
            yield break;

        isSpinning = true;

        targetIndex = WrapIndex(targetIndex);

        // Scrolling down moves the middle symbol backward
        // through the fixed sequence.
        int stepsToTarget =
            WrapIndex(middleIndex - targetIndex);

        int totalSteps = minimumExtraSteps;

        // Ensure the final step lands exactly on the target.
        totalSteps += WrapIndex(stepsToTarget - totalSteps);

        float elapsed = 0f;

        for (int step = 0; step < totalSteps; step++)
        {
            float progress = Mathf.Clamp01(elapsed / spinDuration);

            float stepDuration = Mathf.Lerp(
                fastStepDuration,
                slowStepDuration,
                progress * progress
            );

            float stepElapsed = 0f;

            while (stepElapsed < stepDuration)
            {
                stepElapsed += Time.deltaTime;

                float t = Mathf.Clamp01(stepElapsed / stepDuration);

                // Smooth movement during each symbol step.
                float eased = t * t * (3f - 2f * t);
                SetOffset(eased * symbolHeight);

                elapsed += Time.deltaTime;
                yield return null;
            }

            // Finish this step and advance the fixed symbol sequence.
            SetOffset(symbolHeight);

            middleIndex = WrapIndex(middleIndex - 1);

            ResetPositions();
            SetVisibleSymbols();
        }

        // Exact result check.
        middleIndex = targetIndex;
        ResetPositions();
        SetVisibleSymbols();

        isSpinning = false;
    }
}