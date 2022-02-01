using UnityEngine;

public class LazerGenerator : MonoBehaviour
{
    public bool IsActive { get; private set; } = true;

    [SerializeField] private LayerMask _wallsMask;
    [SerializeField] private Transform _laser;
    [SerializeField] private Transform _laserPoint;


    private void Update()
    {
        if(_laser != null && IsActive)
        {
            if(Physics.Raycast(_laserPoint.position, _laserPoint.forward, out var hit, 100f, _wallsMask, QueryTriggerInteraction.Ignore))
            {
                var distance = Vector3.Distance(_laserPoint.position, hit.point);

                _laser.localScale = new Vector3(1f, 1f, distance);
            }
            else
            {
                _laser.localScale = new Vector3(1f, 1f, 100f);
            }
        }
    }

    public void StopLazer()
    {
        IsActive = false;

        _laser.gameObject.SetActive(IsActive);
    }

    public void StartLazer()
    {
        IsActive = true;

        _laser.gameObject.SetActive(IsActive);
    }
}