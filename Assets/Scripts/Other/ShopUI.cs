using TMPro;
using UnityEngine;

/// <summary>
/// Manage the shop UI at the start.
/// </summary>
public class ShopUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _shopText;

    [SerializeField]
    private BuyingZone _buyingZone;

    private void Start()
    {
        UIController.Instance.UpdateSpecifiedText(_shopText, $"{_buyingZone.SeedForSale.SeedData.Type} seed \n {_buyingZone.SeedForSale.SeedData.Cost}€");
    }
}
