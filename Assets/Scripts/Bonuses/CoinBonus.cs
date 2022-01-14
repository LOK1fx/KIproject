using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBonus : Bonus
{
    [SerializeField] private GameObject _takeEffect;

    private Animator _animator;
    private Collider _collider;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider>();
    }

    protected override void OnGetBonus(Player player)
    {
        if(player.TryGetPawnController<PlayerController>(out var controller))
        {
            controller.AddScore(1);
        }
        else
        {
            return;
        }

        _collider.enabled = false;

        var effect = Instantiate(_takeEffect, transform.position, Quaternion.identity);
        _animator.Play("HPBonus_Take", 0, 0.1f);

        Destroy(effect, 2f);
        Destroy(gameObject, 1f);
    }
}
