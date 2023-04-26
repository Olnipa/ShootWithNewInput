using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCaster : MonoBehaviour
{
    [SerializeField] private MagickCast _cast;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Camera _camera;

    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Player.Shoot.performed += context => OnShoot();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    public void OnShoot()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector3 mousePosition = Mouse.current.position.ReadValue();
            mousePosition.z = _camera.transform.position.z * -1;

            Vector3 mouseClick = _camera.ScreenToWorldPoint(mousePosition);
            mouseClick.z = _shootPoint.transform.position.z;

            Vector3 lookDirection = mouseClick - _shootPoint.position;
            var magickBullet = Instantiate(_cast, _shootPoint.position, Quaternion.LookRotation(lookDirection));
        }
    }
}