using UnityEngine;

/// <summary>
/// Abstract class for the interactions.
/// Not an interface because I need the MonoBehaviour inheritance.
/// </summary>
public abstract class InteractionBase : MonoBehaviour
{
    /// <summary>
    /// Base interaction method.
    /// </summary>
    public abstract void Interact();
}
