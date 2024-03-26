using UnityEngine;

public class BuyingZone : InteractionBase
{
    [SerializeField]
    private Seed _seedForSale;

    public override void Interact(/*Seed currentseed*/)
    {
        Debug.Log("BuyingZone");
    }
}
