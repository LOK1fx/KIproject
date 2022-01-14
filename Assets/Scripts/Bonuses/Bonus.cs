using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Bonus : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            OnGetBonus(player);
        }
    }

    protected abstract void OnGetBonus(Player player);

    private void Update()
    {
        transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);
    }
}