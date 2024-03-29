using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UpdateRebindText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _text;

    [SerializeField]
    private Rebind _rebind;

    [SerializeField]
    private PlayerInput _playerInput;

    private void Start()
    {
        _rebind.OnRebind += UpdateText;
        UpdateText();
    }

    /// <summary>
    /// Update the text with the new input each time the action is rebind.
    /// </summary>
    private void UpdateText()
    {
        _text.text = $"Interact : \n {InputControlPath.ToHumanReadableString(_playerInput.actions["Interact"].bindings[0].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice)}";
    }
}
