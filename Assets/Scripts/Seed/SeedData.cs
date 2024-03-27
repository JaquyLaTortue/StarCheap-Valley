using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SeedData", order = 1)]

/// <summary>
/// Store the seed data, such as the time to grow, cost and price
/// </summary>
public class SeedData : ScriptableObject
{
    [field: SerializeField]
    public string Type { get; private set; }

    [field: SerializeField]
    public Vector2 TimeToGrowRange { get; private set; }

    [field: SerializeField]
    public int Cost { get; private set; }

    [field: SerializeField]
    public int Price { get; private set; }
}
