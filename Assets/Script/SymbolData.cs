using UnityEngine;

public enum SlotSymbol
{
    Cherry,
    Bell,
    Bar,
    Seven
}

[System.Serializable]
public class SymbolData
{
    public SlotSymbol symbol;
    public Sprite sprite;
    [Min(1)] public int payoutMultiplier = 2;
}