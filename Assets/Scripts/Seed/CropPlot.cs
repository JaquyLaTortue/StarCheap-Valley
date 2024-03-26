using UnityEngine;

public class CropPlot : MonoBehaviour
{
    [SerializeField]
    private bool _somethingPlanted = false;

    private bool _isGrowing = false;

    [SerializeField]
    private GameObject _seedPlanted;

    public void PlantSeed(GameObject seed)
    {
        if (!_somethingPlanted)
        {
            _seedPlanted = seed;
            _somethingPlanted = true;
            StartGrowingProcess();
        }
    }

    public void ResetCropPlot()
    {
        _somethingPlanted = false;
        _isGrowing = false;
        _seedPlanted = null;
    }

    private void FixedUpdate()
    {
        if (_somethingPlanted && !_isGrowing)
        {
            StartGrowingProcess();
            _isGrowing = true;
        }
    }

    private void StartGrowingProcess()
    {
    }
}
