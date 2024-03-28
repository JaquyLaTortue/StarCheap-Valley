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
    private float _growTime;

    public event Action<EGrowingStage> OnGrowingStageChange;

    [field: SerializeField]
    public EGrowingStage GrowingStage { get; private set; }

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
        GrowingStage = EGrowingStage.Seed;
        OnGrowingStageChange?.Invoke(GrowingStage);
        yield return new WaitForSeconds(_growTime / 2f);
        GrowingStage = EGrowingStage.Shoot;
        OnGrowingStageChange?.Invoke(GrowingStage);
        Debug.Log("Shoot");
        yield return new WaitForSeconds(_growTime / 2f);
        GrowingStage = EGrowingStage.Plant;
        OnGrowingStageChange?.Invoke(GrowingStage);
        Debug.Log("Plant");
    }

    private void Awake()
    {
        _growTime = Random.Range(SeedData.TimeToGrowRange.x, SeedData.TimeToGrowRange.y);
        GrowingStage = EGrowingStage.Seed;
    }
}
