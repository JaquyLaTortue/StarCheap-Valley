using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

/// <summary>
/// Manage the player's money
/// </summary>
public class PlayerMoney : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _moneyText;

    private bool _notEnoughMoneyCD = true;

    public event Action<int, Color> OnMoneyChange;

    public static PlayerMoney Instance { get; private set; }

    [field: SerializeField]
    public PlayerMain PlayerMain { get; private set; }

    [field: SerializeField]
    public int Money { get; private set; } = 100;

    /// <summary>
    /// Add the specified amount of money
    /// </summary>
    /// <param name="amount"></param>
    public void EarnMoney(int amount)
    {
        Money += amount;
        OnMoneyChange?.Invoke(Money, Color.green);
    }

    /// <summary>
    /// Substract the specified amount of money
    /// </summary>
    /// <param name="amount"></param>
    public void SpendMoney(int amount)
    {
        Money -= amount;
        OnMoneyChange?.Invoke(Money, Color.red);
    }

    /// <summary>
    /// if the player doesn't have enough money, shake the money text and change its color to red
    /// </summary>
    public void NotEnoughMoney()
    {
        if (_notEnoughMoneyCD)
        {
            _notEnoughMoneyCD = false;
            _moneyText.color = Color.red;
            _moneyText.DOColor(Color.black, 1);
            float tweenDuration = 1;
            _moneyText.transform.DOShakePosition(tweenDuration, 10, 10);
            StartCoroutine(NotEnoughMoneyCD(tweenDuration));
        }
    }

    /// <summary>
    /// The cooldown for the not enough money function
    /// </summary>
    /// <returns></returns>
    private IEnumerator NotEnoughMoneyCD(float tweenDuration)
    {
        yield return new WaitForSeconds(tweenDuration);
        _notEnoughMoneyCD = true;
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
