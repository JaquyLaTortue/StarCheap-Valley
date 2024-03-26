using UnityEngine;

public class Planting : InteractionBase
{
    [field: SerializeField]
    public CropPlot CropPlot { get; set; }

    public override void Interact(/*Seed currentseed*/)
    {
        Debug.Log("Planting");
    }
}
