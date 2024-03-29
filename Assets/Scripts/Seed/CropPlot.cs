using System;
using UnityEngine;

/// <summary>
/// The zone where the player can plant seeds.
/// </summary>
public class CropPlot : MonoBehaviour
{
    [SerializeField]
    private GameObject _seedPlanted;

    [Header("Indication")]
    [SerializeField]
    private bool _isActive;

    [SerializeField]
    private GameObject _indicator;

    [SerializeField]
    private GameObject _visibleSeed;

    [SerializeField]
    private Material _seedMaterial;
    [SerializeField]
    private Material _grownMaterial;

    public event Action<string, bool> OnUpdateUI;

    [field: SerializeField]
    public bool SomethingPlanted { get; private set; } = false;

    /// <summary>
    /// Function called by the interaction from the player to the planting script if he have a seed to plant.
    /// </summary>
    /// <param name="seed">The Seed that will be planted in the Crop Plot.</param>
    public void PlantSeed(GameObject seed)
    {
        if (!SomethingPlanted)
        {
            _seedPlanted = seed;

            _seedPlanted.transform.parent = transform;
            _seedPlanted.transform.localPosition = new Vector3(0, 0.5f, 0);
            _seedPlanted.gameObject.SetActive(true);
            Seed current = _seedPlanted.GetComponent<Seed>();
            OnUpdateUI?.Invoke($"Planted a {current.SeedData.Type}", false);

            SomethingPlanted = true;

            current.StartGrowingProcess();
            current.OnGrowingStageChange += ChangeGrowingStage;
        }
    }

    /// <summary>
    /// Function called by the interaction from the player to the planting script if a seed is ready to be harvested.
    /// </summary>
    public void Harvest()
    {
        if (SomethingPlanted && _seedPlanted.GetComponent<Seed>().GrowingStage == EGrowingStage.Plant)
        {
            OnUpdateUI?.Invoke($"Harvested a {_seedPlanted.GetComponent<Seed>().SeedData.Type}", false);
            _seedPlanted.transform.parent = PlayerInventory.Instance.transform;
            _seedPlanted.transform.localPosition = Vector3.zero;
            PlayerInventory.Instance.AddGrownSeed(_seedPlanted.GetComponent<Seed>());
            _seedPlanted.gameObject.SetActive(false);
            ResetCropPlot();
        }
        else
        {
            OnUpdateUI?.Invoke("Not Ready to Harvest", true);
        }
    }

    /// <summary>
    /// Reset the crop plot to its initial state when no Crop plot is near.
    /// </summary>
    public void ResetCropPlot()
    {
        SomethingPlanted = false;
        _seedPlanted = null;
    }

    /// <summary>
    /// Display to the player indications so he can see where he would interact.
    /// </summary>
    /// <param name="state">The state that will define if the indicator is visible or not.</param>
    public void UpdateIndications(bool state)
    {
        _isActive = state;
        _indicator.SetActive(_isActive);
    }

    /// <summary>
    /// Update the Displayed seed according to the growing stage to make the player see the progress.
    /// </summary>
    /// <param name="stage">The actual growing stage of the planted seed.</param>
    private void ChangeGrowingStage(EGrowingStage stage)
    {
        switch (stage)
        {
            case EGrowingStage.Seed:
                _seedPlanted.transform.localPosition = new Vector3(0, 0.5f, 0);
                break;
            case EGrowingStage.Shoot:
                _seedPlanted.transform.localPosition = new Vector3(0, 0.75f, 0);
                break;
            case EGrowingStage.Plant:
                _seedPlanted.transform.localPosition = new Vector3(0, 1, 0);
                break;
            default:
                break;
        }
    }
}
