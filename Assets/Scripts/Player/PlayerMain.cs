using UnityEngine;

/// <summary>
/// Store all the player components.
/// </summary>
public class PlayerMain : MonoBehaviour
{
    [field: SerializeField]
    public PlayerMovement Movement { get; private set; }

    [field: SerializeField]
    public InputsReceiver InputsReceiver { get; private set; }

    [field: SerializeField]
    public PlayerInteract Interact { get; private set; }
}
