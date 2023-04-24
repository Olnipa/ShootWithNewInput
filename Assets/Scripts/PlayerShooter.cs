using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private float _shootDistansce = 1000f;
    [SerializeField] private Debris _debris;

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
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out hit, _shootDistansce)) 
            {
                var debris = Instantiate(_debris, hit.point, Quaternion.identity);
                debris.transform.rotation = Quaternion.FromToRotation(debris.transform.forward, hit.normal) * debris.transform.rotation;
            }
        }
    }
}