using System.Collections;
using System.Collections.Generic;
using EBAC.Core.Singleton;
using TMPro;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    public int coins;

    public TextMeshProUGUI coinCountText;

    private void Start()
    {
        Reset();
        UpdateCoinText(0);
    }

    private void Reset()
    {
        coins = 0;
        UpdateCoinText(coins);
    }

    public void AddCoins(int amount = 1)
    {
        coins += amount;
        UpdateCoinText(coins);
    }

    public void UpdateCoinText(int coinCount)
    {
        coinCountText.text = $"x {coinCount}";
    }
}
