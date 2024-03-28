using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Receive the inputs from the player and cast the corresponding events
/// </summary>
public class InputsReceiver : MonoBehaviour
{
    public event Action<Vector2> OnMove;

    public event Action OnInteract;

    public event Action<float> OnSwitchSeed;

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

    /// <summary>
    /// Cast the SwitchSeed event when the switch seed input is received
    /// </summary>
    /// <param name="ctx"></param>
    public void CastSwitchSeed(InputAction.CallbackContext ctx)
    {
        if (!ctx.started)
        {
            return;
        }

        float value = ctx.ReadValue<float>();
        OnSwitchSeed?.Invoke(value);
    }
}
