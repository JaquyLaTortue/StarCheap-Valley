using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SeedData", order = 1)]

public class SeedData : ScriptableObject
{
    [field: SerializeField]
    public string Type { get; private set; }

    [field: SerializeField]
    public Vector2 TimeToGrowRange { get; private set; }

    [field: SerializeField]
    public Vector2 CostRange { get; private set; }

    [field: SerializeField]
    public Vector2 PriceRange { get; private set; }
}
