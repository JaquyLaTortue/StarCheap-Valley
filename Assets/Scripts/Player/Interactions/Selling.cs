using UnityEngine;

/// <summary>
/// The interaction of selling seed when the player is near the auction house
/// </summary>
public class Selling : InteractionBase
{
    public override void Interact(/*Seed currentseed*/)
    {
        this.DisplayUI(true);
    }
}
