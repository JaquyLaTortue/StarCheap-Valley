using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Store the seed data and manage the growing process
/// </summary>
public class Seed : MonoBehaviour
{
    [SerializeField]
    private EGrowingStage _growingStage;

    [SerializeField]
    private float _growTime;

    public event Action<EGrowingStage> OnGrowingStageChange;

    public int Price { get; private set; }

    [field: SerializeField]
    public SeedData SeedData { get; private set; }

    /// <summary>
    /// Calls the GrowingProcess coroutine to start the growing process
    /// </summary>
    public void StartGrowingProcess()
    {
        StartCoroutine(GrowingProcess());
    }

    /// <summary>
    /// Manage the growing process of the seed
    /// </summary>
    /// <returns></returns>
    private IEnumerator GrowingProcess()
    {
        yield return new WaitForSeconds(_growTime / 2f);
        _growingStage = EGrowingStage.Shoot;
        OnGrowingStageChange?.Invoke(_growingStage);
        Debug.Log("Shoot");
        yield return new WaitForSeconds(_growTime / 2f);
        _growingStage = EGrowingStage.Plant;
        OnGrowingStageChange?.Invoke(_growingStage);
        Debug.Log("Plant");
    }

    private void Awake()
    {
        _growTime = Random.Range(SeedData.TimeToGrowRange.x, SeedData.TimeToGrowRange.y);
        Price = (int)Random.Range(SeedData.PriceRange.x, SeedData.PriceRange.y);
        _growingStage = EGrowingStage.Seed;
    }
}
