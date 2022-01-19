using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DeathZone : MonoBehaviour
{
    [SerializeField] private Damage.Type _damageType = Damage.Type.Void;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<IHealth>(out var health))
        {
            KillActor(health);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<IHealth>(out var health))
        {
            KillActor(health);
        }
    }

    private void KillActor(IHealth health)
    {
        var damage = new Damage(Constants.Gameplay.MAXIMUM_DAMAGE, _damageType);

        health.TakeDamage(damage);
    }
}