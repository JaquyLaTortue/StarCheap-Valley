using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputsReceiver : MonoBehaviour
{
    public event Action<Vector2> OnMove;

    public event Action OnInteract;

    [field: SerializeField]
    public PlayerMain PlayerMain { get; private set; }

    /// <summary>
    /// Cast the Interact event when the interact input is received
    /// </summary>
    /// <param name="ctx"></param>
    public void CastInteract(InputAction.CallbackContext ctx)
    {
        if (!ctx.started)
        {
            return;
        }

        Debug.Log("Interact");
        OnInteract?.Invoke();
    }

    /// <summary>
    /// Cast the Move event when the move input is received
    /// </summary>
    /// <param name="ctx"></param>
    public void CastMove(InputAction.CallbackContext ctx)
    {
        Vector2 direction = ctx.ReadValue<Vector2>();
        OnMove?.Invoke(direction);
    }
}
