using UnityEngine;

/// <summary>
/// Abstract class for the interactions
/// </summary>
public abstract class InteractionBase : MonoBehaviour
{
    [field: SerializeField]
    public GameObject ContextUIParent { get; protected set; }

    /// <summary>
    /// Display the UI of the interaction
    /// </summary>
    public void DisplayUI(bool state)
    {
        ContextUIParent.SetActive(state);
    }

    /// <summary>
    /// Base interaction method
    /// </summary>
    /// <param name="currentseed"></param>
    public abstract void Interact(/*Seed currentseed*/);
}
