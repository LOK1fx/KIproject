using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DeathZone : MonoBehaviour
{
    [SerializeField] private Actor _actor;
    [SerializeField] private Damage.Type _damageType = Damage.Type.Void;

    private void OnCollisionEnter(Collision collision)
    {
        KillActor(collision.collider);
    }

    private void OnCollisionExit(Collision collision)
    {
        KillActor(collision.collider);
    }

    private void OnTriggerEnter(Collider other)
    {
        KillActor(other);
    }

    private void OnTriggerExit(Collider other)
    {
        KillActor(other);
    }

    private void KillActor(Collider collider)
    {
        if (collider.TryGetComponent<IHealth>(out var health))
        {
            var damage = new Damage(Constants.Gameplay.MAXIMUM_DAMAGE, _damageType, _actor);

            health.TakeDamage(damage);
        }   
    }
}