using System;
using UnityEngine;

/// <summary>
/// The zone where the player can buy seeds
/// </summary>
public class BuyingZone : InteractionBase
{
    [SerializeField]
    private Seed _seedForSale;

    [SerializeField]
    private GameObject _indicator;

    [SerializeField]
    private bool _currentBuyZone;

    public event Action<string> OnUpdateUI;

    [field: SerializeField]
    public GameObject SeedPrefab { get; private set; }

    /// <summary>
    /// When the player is near the buying zone, the player can buy the seed that is filled in the inspector
    /// </summary>
    public override void Interact()
    {
        if (PlayerMoney.Instance.Money >= _seedForSale.SeedData.Cost)
        {
            GameObject newSeed = Instantiate(SeedPrefab, PlayerInventory.Instance.transform);

            PlayerMoney.Instance.SpendMoney(_seedForSale.SeedData.Cost);
            PlayerInventory.Instance.AddSeed(newSeed.GetComponent<Seed>());
            OnUpdateUI?.Invoke($"Bought a {_seedForSale.SeedData.Type}");
        }
        else
        {
            PlayerMoney.Instance.NotEnoughMoney();
            OnUpdateUI?.Invoke($"Not Enough Money to buy a {_seedForSale.SeedData.Type} " +
                $"\n (missing {_seedForSale.SeedData.Price - PlayerMoney.Instance.Money})");
        }
    }

    /// <summary>
    /// Display to the player indications so he can see where he would interact
    /// </summary>
    /// <param name="state"></param>
    public void UpdateIndications(bool state)
    {
        _currentBuyZone = state;
        _indicator.SetActive(_currentBuyZone);
    }
}
