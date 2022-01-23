using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public bool OnGround { get; private set; }

    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Vector3 _groundCheckerPosition;
    [SerializeField] private Vector3 _groundCheckerSize;

    private void FixedUpdate()
    {
        var center = transform.position + _groundCheckerPosition;

        if (Physics.CheckBox(center, _groundCheckerSize, Quaternion.identity, _groundMask, QueryTriggerInteraction.Ignore))
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
        Gizmos.DrawWireCube(transform.position + _groundCheckerPosition, _groundCheckerSize * 2);
    }
}
