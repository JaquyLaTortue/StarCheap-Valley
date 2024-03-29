using System;
using TMPro;
using UnityEngine;

/// <summary>
/// Manage the player's money.
/// </summary>
public class PlayerMoney : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _moneyText;

    public event Action<int, bool> OnMoneyChange;

    public static PlayerMoney Instance { get; private set; }

    [field: SerializeField]
    public PlayerMain PlayerMain { get; private set; }

    [field: SerializeField]
    public int Money { get; private set; } = 100;

    /// <summary>
    /// Add the specified amount of money.
    /// </summary>
    /// <param name="amount">The amount of money that will be added.</param>
    public void EarnMoney(int amount)
    {
        Money += amount;
        OnMoneyChange?.Invoke(Money, false);
    }

    /// <summary>
    /// Substract the specified amount of money.
    /// </summary>
    /// <param name="amount">The amount of money that will be substracted.</param>
    public void SpendMoney(int amount)
    {
        Money -= amount;
        OnMoneyChange?.Invoke(Money, false);
    }

    /// <summary>
    /// if the player doesn't have enough money.
    /// </summary>
    public void NotEnoughMoney()
    {
        OnMoneyChange?.Invoke(Money, true);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
}
