using UnityEngine;

/// <summary>
/// Update the animator of the player.
/// </summary>
public class AnimController : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    [field: SerializeField]
    public PlayerMain PlayerMain { get; private set; }

    private void Start()
    {
        PlayerMain.Movement.OnMove += UpdateRunnningAnimator;
    }

    /// <summary>
    /// Update the running animator to the given state.
    /// </summary>
    /// <param name="state">The state that will be use to set the running parametre animation.</param>
    private void UpdateRunnningAnimator(bool state)
    {
        _animator.SetBool("IsRunning", state);
    }
}
