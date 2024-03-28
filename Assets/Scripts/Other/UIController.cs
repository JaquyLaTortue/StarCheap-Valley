using System.Collections;
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
    private PlantingAndHarvest _planting;

    [SerializeField]
    private List<CropPlot> _cropPlots;

    [SerializeField]
    private List<BuyingZone> _buyingZone;

    private bool _sellCd = true;

    public static UIController Instance { get; private set; }

    public void UpdateMoneyText(int money, Color color)
    {
        _moneyText.color = color;
        _moneyText.text = $"Money: {money}€";
        _moneyText.transform.DOShakePosition(0.5f, 0.1f, 10, 90, false);
        _moneyText.DOColor(Color.black, 0.5f).SetDelay(0.5f);
    }

    public void UpdateSeedText(string currentseed)
    {
        _seedText.color = Color.blue;
        _seedText.text = currentseed;
        _seedText.transform.DOShakePosition(0.5f, 0.1f, 10, 90, false);
        _seedText.DOColor(Color.black, 0.5f).SetDelay(0.5f);
    }

    public void UpdateSellText(bool state, string text, Color color)
    {
        _sellText.color = color;
        if (!state && _sellCd)
        {
            _sellCd = false;
            float tweenDuration = 1f;
            _sellText.text = text;
            _sellText.transform.DOShakePosition(tweenDuration, 10, 10);
            _sellText.DOColor(Color.black, tweenDuration);
            StartCoroutine(SellCD(tweenDuration));
            return;
        }

        _sellText.text = text;
        _sellText.DOColor(Color.black, 0.5f).SetDelay(0.5f);
    }

    public void UpdateActionText(string text)
    {
        _actionText.color = Color.blue;
        _actionText.text = text;
        _actionText.transform.DOShakePosition(0.5f, 0.1f, 10, 90, false);
        _actionText.DOColor(Color.black, 0.5f).SetDelay(0.5f);
    }

    public void UpdateSpecifiedText(TMP_Text txt, string text)
    {
        txt.text = text;
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
        UpdateMoneyText(PlayerMoney.Instance.Money, Color.black);

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
}
