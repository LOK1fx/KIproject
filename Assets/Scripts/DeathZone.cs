using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DeathZone : MonoBehaviour
{
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
        health.TakeDamage(Constants.Values.MAXIMUM_DAMAGE);
    }
}