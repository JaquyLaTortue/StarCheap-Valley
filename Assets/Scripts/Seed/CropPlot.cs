using UnityEngine;

/// <summary>
/// The zone where the player can plant seeds
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

    [field: SerializeField]
    public bool SomethingPlanted { get; private set; } = false;

    /// <summary>
    /// Function called by the interaction from the player to the planting script
    /// </summary>
    /// <param name="seed"></param>
    public void PlantSeed(GameObject seed)
    {
        if (!SomethingPlanted)
        {
            seed.transform.parent = transform;
            Seed current = seed.GetComponent<Seed>();

            _seedPlanted = seed;
            SomethingPlanted = true;
            _visibleSeed.SetActive(true);

            current.StartGrowingProcess();
            current.OnGrowingStageChange += ChangeGrowingStage;
        }
    }

    public void Harvest()
    {
        if (SomethingPlanted && _seedPlanted.GetComponent<Seed>()._growingStage == EGrowingStage.Plant)
        {
            Debug.Log("Harvesting");
            _seedPlanted.transform.parent = PlayerInventory.Instance.transform;
            ResetCropPlot();
        }
        else
        {
            Debug.Log("Not Ready to Harvest");
        }
    }

    /// <summary>
    /// Reset the crop plot to its initial state when no Crop plot is near
    /// </summary>
    public void ResetCropPlot()
    {
        SomethingPlanted = false;
        _seedPlanted = null;
        _visibleSeed.SetActive(false);
    }

    /// <summary>
    /// Display to the player indications so he can see where he would interact
    /// </summary>
    /// <param name="state"></param>
    public void UpdateIndications(bool state)
    {
        _isActive = state;
        _indicator.SetActive(_isActive);
    }

    /// <summary>
    /// Update the Displayed seed according to the growing stage to make the player see the progress
    /// </summary>
    /// <param name="stage"></param>
    private void ChangeGrowingStage(EGrowingStage stage)
    {
        switch (stage)
        {
            case EGrowingStage.Seed:
                _visibleSeed.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                break;
            case EGrowingStage.Shoot:
                _visibleSeed.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                break;
            case EGrowingStage.Plant:
                _visibleSeed.transform.localScale = new Vector3(1f, 1f, 1f);
                break;
            default:
                break;
        }
    }
}
