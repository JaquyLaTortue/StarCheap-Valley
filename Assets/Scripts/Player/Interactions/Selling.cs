using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    private Button _defaultSellingButton;

    public event Action<bool, string, Color> OnUpdateUI;

    [field: SerializeField]
    public GameObject ContextUIParent { get; protected set; }

    /// <summary>
    /// Display the UI of the interaction
    /// </summary>
    /// <param name="state"></param>
    public void DisplayUI(bool state)
    {
        ContextUIParent.SetActive(state);
    }

    public override void Interact()
    {
        this.DisplayUI(true);
        _defaultSellingButton.Select();
        _inputHolder.SetActive(false);
        OnUpdateUI?.Invoke(true, "What do you wan't to sell?", Color.blue);
    }

    public void SellGrownSeed(Seed seedBase)
    {
        Dictionary<int, Seed> seedIndex = PlayerInventory.Instance.GetSeedCount(seedBase);
        int count = seedIndex.Count;
        if (count == 0)
        {
            OnUpdateUI?.Invoke(false, $"No {seedBase.SeedData.Type} to sell", Color.red);
            return;
        }

        OnUpdateUI?.Invoke(true, $"Selling {count} {seedBase.SeedData.Type} for {seedBase.SeedData.Price * count}", Color.green);
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
