using UnityEngine;

public class PlayerCamera : MonoBehaviour, IPawnInput
{
    public Player Player;

    private Transform _reserveFollowingObject;

    [SerializeField] private float _sensivity = 12f;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _speed = 80f;
    [SerializeField] private float _maxViewAngle = 70f;

    [Space]
    [SerializeField] private LayerMask _wallMask;
    [SerializeField] private Camera _camera;

    private float _yRotation;
    private float _xRotation;

    private void Start()
    {
        if(Player)
        {
            _yRotation = Player.transform.localRotation.y;
            Player.OnDie += OnPlayerDie;


            transform.position = Player.transform.position - _offset;
        }
    }

    private void OnPlayerDie(Damage damage)
    {
        if(damage.Sender)
        {
            _reserveFollowingObject = damage.Sender.transform;
            _offset = new Vector3(0f, 1.5f, _offset.z);
        }
    }

    private void LateUpdate()
    {
        Vector3 position;

        if(Player)
        {
            position = Player.transform.position;
        }
        else if(_reserveFollowingObject)
        {
            position = _reserveFollowingObject.position;
        }
        else
        {
            position = Vector3.zero;
            _offset = Vector3.zero;
        }

        _camera.transform.localPosition = _offset;

        transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * _speed);
    }

    public void OnInput()
    {
        if(Cursor.lockState == CursorLockMode.Locked)
        {
            _yRotation += (Input.GetAxis("Mouse X") * (_sensivity * 10)) * Time.deltaTime;
            _xRotation -= (Input.GetAxis("Mouse Y") * (_sensivity * 10)) * Time.deltaTime;

            _xRotation = Mathf.Clamp(_xRotation, -_maxViewAngle, _maxViewAngle);

            transform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0f);
            Player.PlayerMovement.DirectionTransform.rotation = Quaternion.Euler(0f, _yRotation, 0f);
        } 
    }

    public void OnPocces()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}