using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovementController : MonoBehaviour
{
    /*
     This script handles Player Control Input and Player Movement+Animations.
     Anything else should be put in different scripts to avoid cluttering.
     */
    PlayerControls _playerControls;

    // special skill
    [SerializeField] float invisibleTime = 5f;
    public bool invisible = false;
    [SerializeField] Material invisibleMaterial;


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
    GunTemplate _gun;
    Transform _gunTransform;

    //Sword
    SwordTemplate _sword;
    Transform _swordTransform;

    //for Weapon Switching, 0 is Gun and 1 is Sword
    int weapon = 0;

    private void Move(InputAction.CallbackContext context)
    {
        //Update movement direction
        _movementInput = context.ReadValue<Vector2>();
        _Movement.x = _movementInput.x;
        _Movement.z = _movementInput.y;
        //For animtion switching
        _isMovementPressed = _movementInput.x != 0 || _movementInput.y != 0;
    }
    private void Aim()
    {
        var (success, posn) = GetMouseRay();
        if (success)
        {
            posn.y += 0.5f;
            Vector3 dir = transform.position - posn;
            _gunTransform.forward = dir;
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
        _gunTransform = transform.Find("Gun");
        _gun = _gunTransform.GetComponent<GunTemplate>();
        _swordTransform = transform.Find("Sword");
        _sword = _swordTransform.GetComponent<SwordTemplate>();


        _playerControls.CharacterControls.Move.started += context => { Move(context); };
        _playerControls.CharacterControls.Move.canceled += context => { Move(context); };
        _playerControls.CharacterControls.Move.performed += context => { Move(context); };
        _playerControls.CharacterControls.Attack.started += context =>
        {
            if (weapon == 0)
            {
                _gun.Shoot();
            }
            if (weapon == 1)
            {
                _sword.Attack();
            }

        };
        _playerControls.CharacterControls.SwitchWeapon.started += context =>
        {
            if (weapon == 0)
            {
                weapon = 1;
            }
            else if (weapon == 1)
            {
                weapon = 0;
            }
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
        _characterController.SimpleMove(_Movement * movementSpeed);

        if (!invisible && Input.GetKeyDown(KeyCode.I))
        {
            StartCoroutine(makeInvisible());
        }
    }

    public IEnumerator makeInvisible()
    {
        Renderer playerRenderer = GetComponent<Renderer>();
        Material originalMaterial = playerRenderer.material;
        playerRenderer.material = invisibleMaterial;

        invisible = true;
        yield return new WaitForSeconds(invisibleTime);
        invisible = false;
        
        playerRenderer.material = originalMaterial;
    }

}

