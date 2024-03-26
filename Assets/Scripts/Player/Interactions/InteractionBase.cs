using UnityEngine;

public abstract class InteractionBase : MonoBehaviour
{
    public GameObject ContextUIParent { get; private set; }

    /// <summary>
    /// Display the UI of the interaction
    /// </summary>
    public void DisplayUI()
    {
        ContextUIParent.SetActive(true);
    }

    /// <summary>
    /// Base interaction method
    /// </summary>
    /// <param name="currentseed"></param>
    public abstract void Interact(/*Seed currentseed*/);
}
