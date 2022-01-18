using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public event Action OnJump;
    public event Action OnLand;

    public bool IsMoving { get; private set; }
    public bool IsMovingHorizontal { get; private set; }
    public float CurrentSpeed { get; private set; }
    public float CurrentHorizontalSpeed { get; private set; }

    [SerializeField] private float _movingThresholdSpeed = 0.1f;
    [SerializeField] private float _jumpForce = 7f;
    [SerializeField] private float _speed = 7f;

    [Space]
    public Transform DirectionTransform;

    private Rigidbody _rigidbody;
    private Player _player;

    private Vector3 _inputDirection;

    private bool _lastGrounded;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _player = GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        Movement();

        CurrentSpeed = _rigidbody.velocity.magnitude;
        CurrentHorizontalSpeed = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z).magnitude;

        if(CurrentSpeed >= _movingThresholdSpeed)
        {
            IsMoving = true;
        }
        else
        {
            IsMoving = false;
        }
        if(CurrentHorizontalSpeed >= _movingThresholdSpeed)
        {
            IsMovingHorizontal = true;
        }
        else
        {
            IsMovingHorizontal = false;
        }
    }

    public void SetInput(Vector2 input)
    {
        _inputDirection = new Vector3(input.x, 0f, input.y);

        _inputDirection = DirectionTransform.transform.TransformDirection(_inputDirection);
    }

    public void Jump()
    {
        if(_player.PlayerState.OnGround)
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);

            OnJump?.Invoke();
        }
    }

    private void Movement()
    {
        if(_player.PlayerState.OnGround)
        {
            _rigidbody.velocity = _inputDirection * _speed;
        }
        else
        {
            _rigidbody.AddForce(_inputDirection * _speed, ForceMode.Acceleration);
        }

        if (_player.PlayerState.OnGround != _lastGrounded && _lastGrounded == false)
        {
            OnLand?.Invoke();
        }

        _lastGrounded = _player.PlayerState.OnGround;
    }

}