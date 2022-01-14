using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private const string ANIM_TRIGGER_JUMP = "Jump";
    private const string ANIM_TRIGGER_LAND = "Land";
    private const string ANIM_PARAM_SPEED = "Speed";
    private const string ANIM_PARAM_ON_GROUND = "OnGround";

    private Animator _animator;
    private Player _player;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _player = GetComponent<Player>();

        _player.PlayerMovement.OnJump += OnJump;
        _player.PlayerMovement.OnLand += OnLand;
    }

    private void Update()
    {
        _animator.SetFloat(ANIM_PARAM_SPEED, _player.PlayerMovement.CurrentSpeed);
        _animator.SetBool(ANIM_PARAM_ON_GROUND, _player.PlayerState.OnGround);
    }

    private void OnJump()
    {
        _animator.SetTrigger(ANIM_TRIGGER_JUMP);
    }

    private void OnLand()
    {
        _animator.SetTrigger(ANIM_TRIGGER_LAND);
    }

    private void OnDisable()
    {
        _player.PlayerMovement.OnJump -= OnJump;
        _player.PlayerMovement.OnLand -= OnLand;
    }

    private void OnDestroy()
    {
        _player.PlayerMovement.OnJump -= OnJump;
        _player.PlayerMovement.OnLand -= OnLand;
    }
}