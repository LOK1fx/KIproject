using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour, IPawnInput
{
    public Player Player;

    [SerializeField] private float _sensivity = 12f;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _maxViewAngle = 70f;

    [Space]
    [SerializeField] private Camera _camera;

    private float _yRotation;
    private float _xRotation;

    private void LateUpdate()
    {
        if(Player)
        {
            _camera.transform.localPosition = _offset;
            transform.position = Player.transform.position;
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