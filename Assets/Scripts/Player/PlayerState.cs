using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public bool OnGround { get; private set; }

    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Vector3 _groundCheckerPosition;
    [SerializeField] private float _groundCheckerRadius = 0.3f;

    private void FixedUpdate()
    {
        if (Physics.CheckSphere(transform.position + _groundCheckerPosition, _groundCheckerRadius, _groundMask))
        {
            OnGround = true;
        }
        else
        {
            OnGround = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + _groundCheckerPosition, _groundCheckerRadius);
    }
}
