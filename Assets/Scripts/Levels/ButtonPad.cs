using System.Collections.Generic;
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

    private List<Rigidbody> _bodiesOnButton = new List<Rigidbody>();

    private void Start()
    {
        _defaultMaterial = _mesh.sharedMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Rigidbody>(out var rigidbody))
        {
            _bodiesOnButton.Add(rigidbody);

            if (_bodiesOnButton.Count == 1)
            {
                _mesh.sharedMaterial = _activatedMaterial;
            
                OnButtonEnter?.Invoke();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Rigidbody>(out var rigidbody))
        {
            _bodiesOnButton.Remove(rigidbody);

            if (_bodiesOnButton.Count == 0)
            {
                _mesh.sharedMaterial = _defaultMaterial;

                OnButtonExit?.Invoke();
            }
        }
    }
}
