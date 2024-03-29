using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Manage the player's inventory.
/// </summary>
public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private InputsReceiver _inputsReceiver;

    [SerializeField]
    private TMP_Text _seedText;

    public event Action OnSeedInventoryModified;

    public event Action<string> OnCurrentSeedUpdated;

    public static PlayerInventory Instance { get; private set; }

    [field: SerializeField]
    public Seed CurrentSeed { get; private set; }

    [field: SerializeField]
    public List<Seed> Seeds { get; private set; }

    [field: SerializeField]
    public List<Seed> GrownSeed { get; private set; }

    /// <summary>
    /// Add the specified seed to the player's inventory.
    /// </summary>
    /// <param name="seedToAdd">The seed that will be added to the player seed inventory.</param>
    public void AddSeed(Seed seedToAdd)
    {
        Seeds.Add(seedToAdd);
        OnSeedInventoryModified?.Invoke();
    }

    /// <summary>
    /// Remove the specified seed from the player's inventory.
    /// </summary>
    /// <param name="seedToRemove">The seed that will be removed from the player seed inventory.</param>
    public void RemoveSeed(Seed seedToRemove)
    {
        Seeds.Remove(seedToRemove);
        if (seedToRemove == CurrentSeed)
        {
            CurrentSeed = null;
            OnSeedInventoryModified?.Invoke();
        }
    }

    /// <summary>
    /// Add the specified grown seed to the player's inventory.
    /// </summary>
    /// <param name="seedToAdd">The grown seed that will be added to the player grown seed inventory.</param>
    public void AddGrownSeed(Seed seedToAdd)
    {
        GrownSeed.Add(seedToAdd);
    }

    /// <summary>
    /// Remove the specified grown seed from the player's inventory.
    /// </summary>
    /// <param name="seedToRemove">The grown seed that will be removeed from the player grown seed inventory.</param>
    public void RemoveGrownSeed(Seed seedToRemove)
    {
        GrownSeed.Remove(seedToRemove);
        Destroy(seedToRemove.gameObject);
    }

    /// <summary>
    /// Get the number of seed of the specified type in the player's inventory with their index.
    /// </summary>
    /// <param name="seed">The Base seed that will be searched in the player grown seed inventory.</param>
    /// <returns>The dictionnay returned contains, in key the number of searched seed, in value another dictionnary wich contains a similar seed to the one searched and its index.</returns>
    public Dictionary<int, Seed> GetSeedCount(Seed seed)
    {
        Dictionary<int, Seed> seedIndex = new ();
        int nbSeed = 0;
        for (int i = 0; i < GrownSeed.Count; i++)
        {
            Seed tempSeed = GrownSeed[i];
            if (tempSeed.SeedData.Type == seed.SeedData.Type)
            {
                seedIndex.Add(nbSeed, tempSeed);
                nbSeed++;
            }
        }

        return seedIndex;
    }

    /// <summary>
    /// Switch the current seed of the player between the ones that he have.
    /// </summary>
    /// <param name="value">The input value.</param>
    private void SwitchCurrentSeed(float value)
    {
        if (Seeds.Count == 0)
        {
            return;
        }

        int index = Seeds.IndexOf(CurrentSeed);
        if (value > 0)
        {
            index++;
            if (index >= Seeds.Count)
            {
                index = 0;
            }
        }
        else
        {
            index--;
            if (index < 0)
            {
                index = Seeds.Count - 1;
            }
        }

        CurrentSeed = Seeds[index];
        OnSeedInventoryModified?.Invoke();
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

        OnSeedInventoryModified += UpdateCurrentSeed;
        _inputsReceiver.OnSwitchSeed += SwitchCurrentSeed;
        UpdateCurrentSeed();
    }

    /// <summary>
    /// Update the current seed of the player if the current is removed.
    /// </summary>
    private void UpdateCurrentSeed()
    {
        if (Seeds.Count == 0)
        {
            OnCurrentSeedUpdated?.Invoke("No seed in inventory");
            return;
        }
        else if (CurrentSeed == null || Seeds.Count == 1)
        {
            CurrentSeed = Seeds[0];
        }

        OnCurrentSeedUpdated?.Invoke($"Selected Seed : {CurrentSeed.SeedData.Type}");
    }
}
