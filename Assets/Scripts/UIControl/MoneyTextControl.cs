using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

/// <summary>
/// Control the money text.
/// </summary>
public class MoneyTextControl : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _moneyText;

    [SerializeField]
    private Color _moneyTextColor;

    [SerializeField]
    private float _shakeDuration = 1f;

    private bool _moneyCd = true;

    private void Start()
    {
        PlayerMoney.Instance.OnMoneyChange += UpdateMoneyText;
        UpdateMoneyText(PlayerMoney.Instance.Money, false);
    }

    /// <summary>
    /// Update the money text.
    /// </summary>
    /// <param name="money">The amount of money that the player have.</param>
    /// <param name="shouldShake">If the text should shake.</param>
    private void UpdateMoneyText(int money, bool shouldShake)
    {
        if (_moneyCd && shouldShake)
        {
            _moneyCd = false;
            _moneyText.color = Color.red;
            _moneyText.text = $"Money: {money}€";
            _moneyText.transform.DOShakePosition(_shakeDuration, 10, 10);
            _moneyText.DOColor(_moneyTextColor, _shakeDuration);
            StartCoroutine(MoneyCD(_shakeDuration));
            return;
        }

        _moneyText.color = Color.green;
        _moneyText.text = $"Money: {money}€";
        _moneyText.transform.DOShakePosition(0.5f, 0.1f, 10, 90, false);
        _moneyText.DOColor(_moneyTextColor, 0.5f).SetDelay(0.5f);
    }

    /// <summary>
    /// The cooldown of the money text to shake again.
    /// </summary>
    /// <param name="tweenDuration">The duration of the cooldown.</param>
    private IEnumerator MoneyCD(float tweenDuration)
    {
        yield return new WaitForSeconds(tweenDuration);
        _moneyCd = true;
    }
}
