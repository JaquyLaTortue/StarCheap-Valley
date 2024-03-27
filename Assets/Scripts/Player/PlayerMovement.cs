using System;
using UnityEngine;

/// <summary>
/// Manage the player's movement
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private float _speed = 5f;

    [SerializeField]
    private Vector3 _direction;

    private Rigidbody _rb;

    public event Action<bool> OnMove;

    [field: SerializeField]
    public PlayerMain PlayerMain { get; private set; }

    private void Awake()
    {
        _rb = GetComponentInChildren<Rigidbody>();
        _rb.velocity = Vector3.zero;

        PlayerMain.InputsReceiver.OnMove += Movement;
    }

    private void Update()
    {
        _rb.velocity = _direction * _speed;
    }

    /// <summary>
    /// Move the player in the given direction and cast the OnMove event to update the animator
    /// </summary>
    /// <param name="tempDirection"></param>
    private void Movement(Vector2 tempDirection)
    {
        _direction = new Vector3(tempDirection.x, 0, tempDirection.y);
        OnMove?.Invoke(_direction != Vector3.zero);
    }
}
