using UnityEngine;

public class RNGManager : MonoBehaviour
{
    // Unity's random generator is used for casual gameplay.
    public int GetRandomSymbolIndex(int symbolCount)
    {
        if (symbolCount <= 0)
        {
            throw new System.ArgumentOutOfRangeException(
                nameof(symbolCount));
        }

        return Random.Range(0, symbolCount);
    }
}