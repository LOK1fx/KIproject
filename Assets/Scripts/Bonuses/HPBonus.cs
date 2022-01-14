using UnityEngine;

public class HPBonus : Bonus
{
    [SerializeField] private GameObject _takeEffect;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    protected override void OnGetBonus(Player player)
    {
        if(player.GetHealth() >= player.MaxHealth)
        {
            return;
        }

        player.AddHealth(1);

        var effect = Instantiate(_takeEffect, transform.position, Quaternion.identity);
        _animator.Play("HPBonus_Take", 0, 0.1f);

        Destroy(effect, 2f);
        Destroy(gameObject, 1f);
    }
}