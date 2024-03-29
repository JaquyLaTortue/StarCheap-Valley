using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

/// <summary>
/// Manage the UI.
/// </summary>
public class UIController : MonoBehaviour
{
    [Header("UI Texts")]
    [SerializeField]
    private TMP_Text _moneyText;

    [SerializeField]
    private TMP_Text _seedText;

    [SerializeField]
    private TMP_Text _sellText;

    [SerializeField]
    private TMP_Text _actionText;

    [Header("References")]
    [SerializeField]
    private Selling _selling;

    [SerializeField]
    private PlantingAndHarvest _planting;

    [SerializeField]
    private List<CropPlot> _cropPlots;

    [SerializeField]
    private List<BuyingZone> _buyingZone;

    [Header("Texts Colors")]
    [SerializeField]
    private Color _sellTextColor;

    [SerializeField]
    private Color _actionTextColor;

    [SerializeField]
    private Color _moneyTextColor;

    [SerializeField]
    private Color _seedTextColor;

    [Header("Cooldowns")]
    private bool _sellCd = true;

    private bool _buyCd = true;

    private bool _moneyCd = true;

    public static UIController Instance { get; private set; }

    /// <summary>
    /// Update the specified text.
    /// </summary>
    /// <param name="txt">The TMP_Text that will be updated.</param>
    /// <param name="text">The message that will be displayed in the specified TMP_Text.</param>
    public void UpdateSpecifiedText(TMP_Text txt, string text)
    {
        txt.text = text;
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
            float tweenDuration = 1f;
            _moneyText.transform.DOShakePosition(tweenDuration, 10, 10);
            _moneyText.DOColor(_moneyTextColor, tweenDuration);
            StartCoroutine(MoneyCD(tweenDuration));
            return;
        }

        _moneyText.color = Color.green;
        _moneyText.text = $"Money: {money}€";
        _moneyText.transform.DOShakePosition(0.5f, 0.1f, 10, 90, false);
        _moneyText.DOColor(_moneyTextColor, 0.5f).SetDelay(0.5f);
    }

    /// <summary>
    /// Update the seed text.
    /// </summary>
    /// <param name="currentseed">The message that will be displayed.</param>
    private void UpdateSeedText(string currentseed)
    {
        _seedText.color = Color.black;
        _seedText.text = currentseed;
        _seedText.transform.DOShakePosition(0.5f, 0.1f, 10, 90, false);
        _seedText.DOColor(_seedTextColor, 0.5f);
    }

    /// <summary>
    /// Update the sell text.
    /// </summary>
    /// <param name="shouldShake">If the text should shake.</param>
    /// <param name="text">The message that will be displayed.</param>
    /// <param name="color">The color in wich the message should be diplayed.</param>
    private void UpdateSellText(bool shouldShake, string text, Color color)
    {
        _sellText.color = color;
        if (!shouldShake && _sellCd)
        {
            _sellCd = false;
            float tweenDuration = 1f;
            _sellText.text = text;
            _sellText.transform.DOShakePosition(tweenDuration, 10, 10);
            _sellText.DOColor(_sellTextColor, tweenDuration);
            StartCoroutine(SellCD(tweenDuration));
            return;
        }

        _sellText.text = text;
        _sellText.DOColor(_sellTextColor, 0.5f).SetDelay(0.5f);
    }

    /// <summary>
    /// Update the action text.
    /// </summary>
    /// <param name="text">The message that will be displayed.</param>
    /// <param name="shouldShake">If the text should shake.</param>
    private void UpdateActionText(string text, bool shouldShake)
    {
        if (_buyCd && shouldShake)
        {
            _buyCd = false;
            float tweenDuration = 1f;
            _actionText.color = Color.red;
            _actionText.text = text;
            _actionText.transform.DOShakePosition(tweenDuration, 10, 10);
            _actionText.DOColor(_actionTextColor, tweenDuration);
            StartCoroutine(BuyCD(tweenDuration));
            return;
        }

        _actionText.color = Color.black;
        _actionText.text = text;
        _actionText.DOColor(_actionTextColor, 0.5f);
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

    private void Start()
    {
        PlayerMoney.Instance.OnMoneyChange += UpdateMoneyText;
        UpdateMoneyText(PlayerMoney.Instance.Money, false);

        PlayerInventory.Instance.OnCurrentSeedUpdated += UpdateSeedText;
        UpdateSeedText("No seed in inventory");

        _planting.OnUpdateUI += UpdateActionText;

        _selling.OnUpdateUI += UpdateSellText;

        foreach (var item in _cropPlots)
        {
            item.OnUpdateUI += UpdateActionText;
        }

        foreach (var item in _buyingZone)
        {
            item.OnUpdateUI += UpdateActionText;
        }

        _sellText.text = string.Empty;
    }

    private IEnumerator SellCD(float tweenDuration)
    {
        yield return new WaitForSeconds(tweenDuration);
        _sellCd = true;
    }

    private IEnumerator BuyCD(float tweenDuration)
    {
        yield return new WaitForSeconds(tweenDuration);
        _buyCd = true;
    }

    private IEnumerator MoneyCD(float tweenDuration)
    {
        yield return new WaitForSeconds(tweenDuration);
        _moneyCd = true;
    }
}
