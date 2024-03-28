using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

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
    private List<CropPlot> _cropPlots;

    [SerializeField]
    private List<BuyingZone> _buyingZone;

    public static UIController Instance { get; private set; }

    public void UpdateMoneyText(int money, Color color)
    {
        _moneyText.color = color;
        _moneyText.text = money.ToString();
        transform.DOShakePosition(0.5f, 0.1f, 10, 90, false);
        _moneyText.DOColor(Color.black, 0.5f).SetDelay(0.5f);
    }

    public void UpdateSeedText(string currentseed)
    {
        _seedText.color = Color.blue;
        _seedText.text = currentseed;
        transform.DOShakePosition(0.5f, 0.1f, 10, 90, false);
        _seedText.DOColor(Color.black, 0.5f).SetDelay(0.5f);
    }

    public void UpdateSellText(string text)
    {
        _sellText.color = Color.green;
        _sellText.text = text;
        transform.DOShakePosition(0.5f, 0.1f, 10, 90, false);
        _sellText.DOColor(Color.black, 0.5f).SetDelay(0.5f);
    }

    public void UpdateActionText(string text)
    {
        _actionText.color = Color.blue;
        _actionText.text = text;
        transform.DOShakePosition(0.5f, 0.1f, 10, 90, false);
        _actionText.DOColor(Color.black, 0.5f).SetDelay(0.5f);
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

        PlayerMoney.Instance.OnMoneyChange += UpdateMoneyText;
        _moneyText.text = PlayerMoney.Instance.Money.ToString();

        PlayerInventory.Instance.OnCurrentSeedUpdated += UpdateSeedText;
        _seedText.text = "No seed in inventory";

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
}
