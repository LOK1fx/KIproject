using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private const string ANIM_TRIGGER_JUMP = "Jump";
    private const string ANIM_TRIGGER_LAND = "Land";
    private const string ANIM_PARAM_SPEED = "Speed";
    private const string ANIM_PARAM_ON_GROUND = "OnGround";

    private Animator _animator;
    private PlayerMovement _playerMovement;
    private PlayerState _playerState;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerState = GetComponent<PlayerState>();

        _playerMovement.OnJump += OnJump;
        _playerMovement.OnLand += OnLand;
    }

    private void Update()
    {
        _animator.SetFloat(ANIM_PARAM_SPEED, _playerMovement.CurrentSpeed);
        _animator.SetBool(ANIM_PARAM_ON_GROUND, _playerState.OnGround);
    }

    private void OnJump()
    {
        _animator.SetTrigger(ANIM_TRIGGER_JUMP);
    }

    private void OnLand(Vector3 velocity)
    {
        _animator.SetTrigger(ANIM_TRIGGER_LAND);
    }

    private void OnDisable()
    {
        _playerMovement.OnJump -= OnJump;
        _playerMovement.OnLand -= OnLand;
    }

    private void OnDestroy()
    {
        _playerMovement.OnJump -= OnJump;
        _playerMovement.OnLand -= OnLand;
    }
}