using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FallingBlock : MonoBehaviour
{
    [SerializeField] private float _speed;

    private bool _isFalling;

    private Collider _collider;

    private void Start()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.TryGetComponent<Pawn>(out var pawn))
        {
            _isFalling = true;
            _collider.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        if(_isFalling)
        {
            transform.position = transform.position -= new Vector3(0f, _speed, 0f);
        }
        else
        {
            return;
        }

        if(transform.position.y <= -100f)
        {
            Destroy(gameObject);
        }
    }
}