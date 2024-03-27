using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manage the player's inventory
/// </summary>
public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; }

    public event Action OnInventoryModified;

    [field: SerializeField]
    public Seed CurrentSeed { get; private set; }

    [field: SerializeField]
    public List<Seed> Seeds { get; private set; }

    /// <summary>
    /// Add the specified seed to the player's inventory
    /// </summary>
    /// <param name="seedToAdd"></param>
    public void AddSeed(Seed seedToAdd)
    {
        Seeds.Add(seedToAdd);
        OnInventoryModified?.Invoke();
    }

    /// <summary>
    /// Remove the specified seed from the player's inventory
    /// </summary>
    /// <param name="seedToRemove"></param>
    public void RemoveSeed(Seed seedToRemove)
    {
        Seeds.Remove(seedToRemove);
        if (seedToRemove == CurrentSeed)
        {
            CurrentSeed = null;
            OnInventoryModified?.Invoke();
        }
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

        OnInventoryModified += UpdateCurrentSeed;
    }

    /// <summary>
    /// Update the current seed of the player if the current is removed
    /// </summary>
    private void UpdateCurrentSeed()
    {
        if (Seeds.Count == 1)
        {
            CurrentSeed = Seeds[0];
        }
    }
}
