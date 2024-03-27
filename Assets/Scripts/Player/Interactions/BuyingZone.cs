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

    [field: SerializeField]
    public GameObject SeedPrefab { get; private set; }

    /// <summary>
    /// When the player is near the buying zone, the player can buy the seed that is filled in the inspector
    /// </summary>
    public override void Interact(/*Seed currentseed*/)
    {
        if (PlayerMoney.Instance.Money >= _seedForSale.Price)
        {
            GameObject newSeed = Instantiate(SeedPrefab);

            PlayerMoney.Instance.SpendMoney(_seedForSale.Price);
            PlayerInventory.Instance.AddSeed(newSeed.GetComponent<Seed>());
        }
        else
        {
            PlayerMoney.Instance.NotEnoughMoney();
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
