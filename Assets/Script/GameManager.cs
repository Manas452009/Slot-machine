using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Reel[] reels;
    [SerializeField] private RNGManager rngManager;
    [SerializeField] private UIManager uiManager;
   // [SerializeField] private SlotHandle slotHandle;

    [SerializeField] private int startingBalance = 1000;
    [SerializeField] private int currentBet = 10;

    private int balance;
    private bool isSpinning;

    private void Start()
    {
        balance = startingBalance;
        uiManager.UpdateBalance(balance);
        uiManager.UpdateBet(currentBet);
    }

    public void Spin()
    {
        //slotHandle.Pull();
        if (isSpinning || balance < currentBet)
            return;

        StartCoroutine(SpinReels());
    }

    private IEnumerator SpinReels()
    {
        isSpinning = true;

        balance -= currentBet;
        uiManager.UpdateBalance(balance);
        uiManager.ShowMessage("Good luck!");

        int[] outcomes = new int[reels.Length];

        // Decide all reel outcomes before spinning.
        for (int i = 0; i < reels.Length; i++)
        {
            outcomes[i] =
                rngManager.GetRandomSymbolIndex(6);
        }

        // Start every reel at the same time.
        for (int i = 0; i < reels.Length; i++)
        {
            StartCoroutine(reels[i].Spin(outcomes[i]));
        }

        // Wait until all reels have finished spinning.
        yield return new WaitUntil(() =>
        {
            foreach (Reel reel in reels)
            {
                if (reel.IsSpinning)
                    return false;
            }

            return true;
        });

        // Check the result only after all reels stop.
        CheckWin();

        isSpinning = false;
    }


    private void CheckWin()
    {
        SlotSymbol first = reels[0].CurrentSymbol;

        for (int i = 1; i < reels.Length; i++)
        {
            if (reels[i].CurrentSymbol != first)
            {
                uiManager.ShowMessage("Try again!");
                return;
            }
        }

        int multiplier = GetPayoutMultiplier(first);
        int winnings = currentBet * multiplier;

        balance += winnings;
        uiManager.UpdateBalance(balance);
        uiManager.ShowMessage($"WIN! +{winnings} coins");
    }


    private int GetPayoutMultiplier(SlotSymbol symbol)
    {
        switch (symbol)
        {
            case SlotSymbol.Seven:   return 5;
            case SlotSymbol.Bar:     return 5;
            case SlotSymbol.Bell:    return 5;
            case SlotSymbol.Cherry:  return 5;
            default:                 return 0;
        }
    }

    public void ChangeBet(int amount)
    {
        if (isSpinning)
            return;

        currentBet = Mathf.Clamp(currentBet + amount, 10, 100);
        uiManager.UpdateBet(currentBet);
    }

    public void exitgame(){
         Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}