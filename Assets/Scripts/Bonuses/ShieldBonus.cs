using UnityEngine;

public class ShieldBonus : Bonus
{
    [SerializeField] private GameObject _shieldEffect;
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
        var modifier = new ShieldBonusModifier(this);

        modifier.MakePlayerInvicible = true;
        modifier.CanStuck = false;

        player.ApplyModifier(modifier);

        var playerEffect = Instantiate(_shieldEffect, player.transform);

        _collider.enabled = false;

        var effect = Instantiate(_takeEffect, transform.position, Quaternion.identity);
        _animator.Play("HPBonus_Take", 0, 0.1f);

        Destroy(effect, 2f);
        Destroy(gameObject, 1f);
    }
}
