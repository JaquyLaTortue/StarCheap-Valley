using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The interaction of selling seed when the player is near the auction house.
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
    /// Display the UI of the interaction.
    /// </summary>
    /// <param name="state">The state that define if the UI that will be displayed.</param>
    public void DisplayUI(bool state)
    {
        ContextUIParent.SetActive(state);
    }

    /// <summary>
    /// Called when the player is near the auction house and interact.
    /// It will display the UI so the player can sell his grown seed.
    /// </summary>
    public override void Interact()
    {
        DisplayUI(true);
        _defaultSellingButton.Select();
        _inputHolder.SetActive(false);
        OnUpdateUI?.Invoke(true, "What do you wan't to sell?", Color.white);
    }

    /// <summary>
    /// Sell every grown seed that has the same type as the specified seed.
    /// </summary>
    /// <param name="seedBase">The base seed that will serve to search similar seeds.</param>
    public void SellGrownSeed(Seed seedBase)
    {
        Dictionary<int, Seed> seedIndex = PlayerInventory.Instance.GetSeedCount(seedBase);
        int count = seedIndex.Count;
        if (count == 0)
        {
            OnUpdateUI?.Invoke(false, $"No {seedBase.SeedData.Type} to sell", Color.red);
            return;
        }

        OnUpdateUI?.Invoke(true, $"Selling {count} {seedBase.SeedData.Type} \n for {seedBase.SeedData.Price * count}", Color.green);
        for (int i = 0; i < count; i++)
        {
            PlayerInventory.Instance.RemoveGrownSeed(seedIndex[i]);
            PlayerMoney.Instance.EarnMoney(seedBase.SeedData.Price);
        }

        GC.Collect();
    }

    /// <summary>
    /// Update the indications so the player can see where he can interact.
    /// </summary>
    /// <param name="state">The state that define if the indicator is visible.</param>
    public void UpdateIndications(bool state)
    {
        _indicator.SetActive(state);
    }
}
