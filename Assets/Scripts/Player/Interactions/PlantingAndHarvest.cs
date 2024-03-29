using System;
using UnityEngine;

/// <summary>
/// The interaction of planting seeds in the crop plots.
/// </summary>
public class PlantingAndHarvest : InteractionBase
{
    [SerializeField]
    private CropPlot _currentCropPlot;

    public event Action<string, bool> OnUpdateUI;

    /// <summary>
    /// When the player is near a crop plot, the player can plant the current seed of the inventory.
    /// </summary>
    public override void Interact()
    {
        if (_currentCropPlot == null)
        {
            Debug.LogError("No crop plot selected (ShouldNotPass Here");
            return;
        }

        if (_currentCropPlot.SomethingPlanted)
        {
            _currentCropPlot.Harvest();
            return;
        }

        if (PlayerInventory.Instance.CurrentSeed == null)
        {
            OnUpdateUI?.Invoke("No seed actually selected", true);
            return;
        }

        Seed currentSeed = PlayerInventory.Instance.CurrentSeed;
        _currentCropPlot.PlantSeed(currentSeed.gameObject);
        PlayerInventory.Instance.RemoveSeed(currentSeed);
    }

    /// <summary>
    /// Define the current crop plot that the player is near and call the UpdateIndications method.
    /// </summary>
    /// <param name="current">The current closest Crop Plot in range.</param>
    public void DefineCurrentCropPlot(CropPlot current)
    {
        _currentCropPlot = current;
        current.UpdateIndications(true);
    }

    /// <summary>
    /// Reset the previous current crop plot when it is no more near.
    /// </summary>
    public void ResetCropPlot()
    {
        _currentCropPlot.UpdateIndications(false);
        _currentCropPlot = null;
    }
}
