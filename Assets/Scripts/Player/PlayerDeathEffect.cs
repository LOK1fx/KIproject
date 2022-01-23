using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public abstract class PlayerDeathEffect : MonoBehaviour
{
    [SerializeField] protected GameObject effectPrefab;

    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();

        _player.OnDie += OnPlayerDie;
    }

    protected abstract void OnPlayerDie(Damage type);
}
