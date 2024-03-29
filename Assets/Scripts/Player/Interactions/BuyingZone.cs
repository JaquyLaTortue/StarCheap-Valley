using System;
using UnityEngine;

/// <summary>
/// The zone where the player can buy seeds.
/// </summary>
public class BuyingZone : InteractionBase
{
    [SerializeField]
    private GameObject _indicator;

    [SerializeField]
    private bool _currentBuyZone;

    public event Action<string, bool> OnUpdateUI;

    [field: SerializeField]
    public GameObject SeedPrefab { get; private set; }

    [field: SerializeField]
    public Seed SeedForSale { get; private set; }

    /// <summary>
    /// When the player is near the buying zone, the player can buy the seed that is filled in the inspector.
    /// </summary>
    public override void Interact()
    {
        if (PlayerMoney.Instance.Money >= SeedForSale.SeedData.Cost)
        {
            GameObject newSeed = Instantiate(SeedPrefab, PlayerInventory.Instance.transform);
            newSeed.gameObject.SetActive(false);

            PlayerMoney.Instance.SpendMoney(SeedForSale.SeedData.Cost);
            PlayerInventory.Instance.AddSeed(newSeed.GetComponent<Seed>());
            OnUpdateUI?.Invoke($"Bought a {SeedForSale.SeedData.Type}", false);
        }
        else
        {
            PlayerMoney.Instance.NotEnoughMoney();
            OnUpdateUI?.Invoke($"Not Enough Money to buy a {SeedForSale.SeedData.Type} \n (missing {SeedForSale.SeedData.Cost - PlayerMoney.Instance.Money}€)", true);
        }
    }

    /// <summary>
    /// Display to the player indications so he can see where he would interact.
    /// </summary>
    /// <param name="state">The state that will define if the indicator is visble or not.</param>
    public void UpdateIndications(bool state)
    {
        _currentBuyZone = state;
        _indicator.SetActive(_currentBuyZone);
    }
}
