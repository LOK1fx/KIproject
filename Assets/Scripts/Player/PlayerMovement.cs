using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(PlayerState))]
public class PlayerMovement : MonoBehaviour
{
    public event Action OnJump;
    public event Action<Vector3> OnLand;

    public Vector3 InputDirection { get; private set; }
    public Vector3 Velocity { get; private set; }

    public bool IsMoving { get; private set; }
    public bool IsMovingHorizontal { get; private set; }
    public float CurrentSpeed { get; private set; }
    public float CurrentHorizontalSpeed { get; private set; }

    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _movingThresholdSpeed = 0.1f;
    [SerializeField] private float _jumpForce = 7f;
    [SerializeField] private float _speed = 7f;

    [Space]
    public Transform DirectionTransform;

    private Rigidbody _rigidbody;
    private PlayerState _state;

    private bool _lastGrounded;
    private RaycastHit _slopeHit;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _state = GetComponent<PlayerState>();
    }

    private void FixedUpdate()
    {
        Movement();

        CurrentSpeed = _rigidbody.velocity.magnitude;
        CurrentHorizontalSpeed = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z).magnitude;
        Velocity = _rigidbody.velocity;

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
        InputDirection = new Vector3(input.x, 0f, input.y);

        InputDirection = DirectionTransform.transform.TransformDirection(InputDirection).normalized;
    }

    public void Jump()
    {
        if(_state.OnGround)
        {
            var velocity = _rigidbody.velocity;

            _rigidbody.velocity = new Vector3(velocity.x, 0f, velocity.z);
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);

            OnJump?.Invoke();
        }
    }

    private void Movement()
    {
        //_rigidbody.useGravity = !_player.PlayerState.OnGround;

        if(_state.OnGround)
        {
            GroundMove(OnSlope());
        }
        else
        {
            AirMove();
        }

        if (_state.OnGround != _lastGrounded && _lastGrounded == false)
        {
            OnLand?.Invoke(_rigidbody.velocity);
        }

        _lastGrounded = _state.OnGround;
    }
    
    private void GroundMove(bool onSlope)
    {
        Vector3 moveDir;

        if(onSlope)
        {
            moveDir = Project(InputDirection, _slopeHit.normal);
        }
        else
        {
            moveDir = InputDirection;
        }

        _rigidbody.velocity = moveDir * _speed;
    }

    private void AirMove()
    {
        _rigidbody.AddForce(InputDirection * _speed, ForceMode.Acceleration);
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, -transform.up, out _slopeHit, 0.7f, _groundMask))
        {
            if (_slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private Vector3 Project(Vector3 dir, Vector3 normal)
    {
        return dir - Vector3.Dot(dir, normal) * normal;
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        if(InputDirection == Vector3.zero)
        {
            return;
        }

        if (!OnSlope()) { return; }

        var pos = transform.position + (Vector3.down * 0.5f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(pos, pos + _slopeHit.normal * 3f);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(pos, pos + Project(InputDirection, _slopeHit.normal) * 3f);
    }

#endif
}