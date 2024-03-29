using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MashingRebind : MonoBehaviour
{
    // The UI that will be displayed when the player will rebind his keys
    [SerializeField]
    private GameObject _uIRebind;

    private InputActionRebindingExtensions.RebindingOperation _rebindingOperation;

    [Header("Gamepad Button")]
    [SerializeField]
    private GameObject[] _gamepadButton = new GameObject[4];

    public event Action OnRebind;

    /// <summary>
    /// Rebind the keyboard.
    /// </summary>
    /// <param name="inputActionref">The input action reference to rebind.</param>
    public void RebindKeyboard(InputActionReference inputActionref)
    {
        InputAction action = inputActionref.action;
        OnRebind?.Invoke();
        action.Disable();
        _rebindingOperation = inputActionref.action.PerformInteractiveRebinding(0)
            .WithControlsExcluding("Mouse")
            .WithControlsExcluding("Gamepad")
            .WithControlsExcluding("Touchpad")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindComplete(action))
            .Start();
    }

    /// <summary>
    /// Called when the rebinding is complete
    /// </summary>
    private void RebindComplete(InputAction action)
    {
        InputBinding binding = action.bindings[0];
        binding.overridePath = _rebindingOperation.action.bindings[0].effectivePath;
        action.ApplyBindingOverride(0, binding);

        action.Enable();
        _rebindingOperation.Dispose();
        _uIRebind.SetActive(false);
    }

    /// <summary>
    /// Display the UI to rebind the keys.
    /// </summary>
    private void DisplayStartRebind()
    {
        if (_uIRebind.activeSelf)
        {
            _uIRebind.SetActive(false);
        }
        else
        {
            _uIRebind.SetActive(true);
        }
    }
}
