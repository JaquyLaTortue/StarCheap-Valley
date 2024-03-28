using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

/// <summary>
/// The interaction of selling seed when the player is near the auction house
/// </summary>
public class Selling : InteractionBase
{
    [SerializeField]
    private GameObject _inputHolder;

    [SerializeField]
    private TMP_Text _sellText;

    [SerializeField]
    private GameObject _indicator;

    [field: SerializeField]
    public GameObject ContextUIParent { get; protected set; }

    /// <summary>
    /// Display the UI of the interaction
    /// </summary>
    public void DisplayUI(bool state)
    {
        ContextUIParent.SetActive(state);
    }

    public override void Interact()
    {
        this.DisplayUI(true);
        _inputHolder.SetActive(false);
    }

    public void SellGrownSeed(Seed seedBase)
    {
        Debug.Log("Selling");
        Dictionary<int, Seed> seedIndex = PlayerInventory.Instance.GetSeedCount(seedBase);
        int count = seedIndex.Count;
        if (count == 0)
        {
            _sellText.color = Color.red;
            _sellText.text = $"No {seedBase.SeedData.Type} to sell";
            _sellText.DOColor(Color.white, 0.5f).SetDelay(0.5f);
            return;
        }

        _sellText.text = $"Selling {count} {seedBase.SeedData.Type} for {seedBase.SeedData.Price * count}";
        for (int i = 0; i < count; i++)
        {
            PlayerInventory.Instance.RemoveGrownSeed(seedIndex[i]);
            PlayerMoney.Instance.EarnMoney(seedBase.SeedData.Price);
        }
    }

    public void UpdateIndications(bool state)
    {
        _indicator.SetActive(state);
    }
}
