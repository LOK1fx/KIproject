using UnityEngine;

public class PlayerCamera : MonoBehaviour, IPawnInput
{
    public Player Player;

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
        _yRotation = Player.transform.localRotation.y;
    }

    private void LateUpdate()
    {
        if(Player)
        {
            var playerPos = Player.transform.position;

            _camera.transform.localPosition = _offset;

            transform.position = Vector3.Lerp(transform.position, playerPos, Time.deltaTime * _speed);
        }
    }

    public void OnInput()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _yRotation += (Input.GetAxis("Mouse X") * (_sensivity * 10)) * Time.deltaTime;
        _xRotation -= (Input.GetAxis("Mouse Y") * (_sensivity * 10)) * Time.deltaTime;

        _xRotation = Mathf.Clamp(_xRotation, -_maxViewAngle, _maxViewAngle);

        transform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0f);
        Player.PlayerMovement.DirectionTransform.rotation = Quaternion.Euler(0f, _yRotation, 0f);
    }
}