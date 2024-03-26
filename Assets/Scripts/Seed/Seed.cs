using UnityEngine;

public class Seed : MonoBehaviour
{
    [SerializeField]
    private EGrowingStage _growingStage;

    [SerializeField]
    private float _growTime;

    [field: SerializeField]
    public SeedData SeedData { get; private set; }

    public void WaitToGrow()
    {
        new WaitForSeconds(_growTime / 2);
        _growingStage = EGrowingStage.Shoot;
        new WaitForSeconds(_growTime / 2);
        _growingStage = EGrowingStage.Plant;
        return;
    }

    private void Awake()
    {
        _growTime = Random.Range(SeedData.TimeToGrowRange.x, SeedData.TimeToGrowRange.y);
        _growingStage = EGrowingStage.Seed;
    }
}
