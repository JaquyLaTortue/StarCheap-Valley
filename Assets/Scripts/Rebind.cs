using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Rebind the keys of the player.
/// </summary>
public class Rebind : MonoBehaviour
{
    // The UI that will be displayed when the player will rebind his keys
    [SerializeField]
    private GameObject _uIRebind;

    private InputActionRebindingExtensions.RebindingOperation _rebindingOperation;

    public event Action OnRebind;

    /// <summary>
    /// rebind the input action reference specified to a new one.
    /// </summary>
    /// <param name="inputActionref">The input action reference to rebind.</param>
    public void RebindKeyboard(InputActionReference inputActionref)
    {
        _uIRebind.SetActive(true);
        InputAction action = inputActionref.action;
        action.Disable();
        _rebindingOperation = inputActionref.action.PerformInteractiveRebinding(0)
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindComplete(action))
            .Start();
    }

    /// <summary>
    /// Called when the rebinding is complete.
    /// </summary>
    private void RebindComplete(InputAction action)
    {
        InputBinding binding = action.bindings[0];
        binding.overridePath = _rebindingOperation.action.bindings[0].effectivePath;
        action.ApplyBindingOverride(0, binding);

        action.Enable();
        _rebindingOperation.Dispose();
        _uIRebind.SetActive(false);
        OnRebind?.Invoke();
    }
}
