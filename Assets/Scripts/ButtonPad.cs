using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class ButtonPad : MonoBehaviour
{
    public UnityEvent OnButtonEnter;
    public UnityEvent OnButtonExit;

    [SerializeField] private SkinnedMeshRenderer _mesh;

    [SerializeField] private Material _activatedMaterial;
    private Material _defaultMaterial;

    private void Start()
    {
        _defaultMaterial = _mesh.sharedMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Rigidbody>(out var rigidbody))
        {
            _mesh.sharedMaterial = _activatedMaterial;

            OnButtonEnter?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Rigidbody>(out var rigidbody))
        {
            _mesh.sharedMaterial = _defaultMaterial;

            OnButtonExit?.Invoke();
        }
    }
}
