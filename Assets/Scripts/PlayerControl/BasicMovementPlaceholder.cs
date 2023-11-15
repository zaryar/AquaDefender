using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class BasicMovementPlaceholder : MonoBehaviour
{
    
    PlayerControls _playerControls;

    //Movement
    CharacterController _characterController;
    Vector2 _movementInput;
    Vector3 _Movement;
    [SerializeField] float movementSpeed = 4;

    //for Animations
    bool _isMovementPressed;

    //Aiming
    [SerializeField] LayerMask groundMask;
    [SerializeField] Camera mainCamera;

    //Gun
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] GameObject ammunition;
    [SerializeField] float bulletSpeed;

    private void Move(InputAction.CallbackContext context)
    {
        _movementInput = context.ReadValue<Vector2>();
        _Movement.x = _movementInput.x;
        _Movement.z = _movementInput.y;
        _isMovementPressed = _movementInput.x != 0 || _movementInput.y != 0;
    }
    private void Aim()
    {
        var (success, posn) = GetMouseRay();
        if (success)
        {
            posn.y += 0.5f;
            Vector3 dir = transform.position - posn;
            dir.y = 0;
            transform.forward = dir;
        }
    }

    private (bool success, Vector3 posn) GetMouseRay()
    {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask))
        {
            return (success: true, posn: hitInfo.point);
        }
        else
        {
            return (success: false, posn: Vector3.zero);
        }
    }

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _characterController = GetComponent<CharacterController>();

        _playerControls.CharacterControls.Move.started += context => { Move(context); };
        _playerControls.CharacterControls.Move.canceled += context => { Move(context); };
        _playerControls.CharacterControls.Move.performed += context => { Move(context); };
        _playerControls.CharacterControls.Attack.started += context => {
            var bullet = Instantiate(ammunition, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
        };
    }

    private void OnEnable()
    {
        _playerControls.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.CharacterControls.Disable();
    }

    private void Update()
    {
        Aim();
    }

    private void FixedUpdate()
    {
        _characterController.SimpleMove(_Movement * movementSpeed);
    }
}

