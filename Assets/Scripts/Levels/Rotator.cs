using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Vector3 _angle;

    private void Update()
    {
        transform.Rotate(_angle * Time.deltaTime);
    }
}