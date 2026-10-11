using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text balanceText;
    [SerializeField] private Text betText;
    [SerializeField] private Text messageText;
    [SerializeField] private Button spinButton;

    public void UpdateBalance(int balance)
    {
        balanceText.text = $"Coins: {balance}";
    }

    public void UpdateBet(int bet)
    {
        betText.text = $"Bet: {bet}";
    }

    public void ShowMessage(string message)
    {
        messageText.text = message;
    }
}