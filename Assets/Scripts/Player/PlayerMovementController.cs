using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Animations.Rigging;
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
    [SerializeField] SkinnedMeshRenderer PlayerRenderer;

    //Movement
    CharacterController _characterController;
    Vector2 _movementInput;
    Vector3 _Movement;
    [SerializeField] float movementSpeed = 4;

    //for Animations
    bool _isRunning;
    bool _isDead = false;
    int _isRunningHash;
    int _dirXHash;
    int _dirZHash;
    [SerializeField] Animator PlayerAnimator;
    [SerializeField] Rig AimRig;

    //Aiming
    [SerializeField] LayerMask groundMask;
    [SerializeField] Camera mainCamera;

    //Gun
    GunTemplate _gun;
    Transform _gunTransform;
    [SerializeField] GameObject GunModel;

    //WaterCannon
    Coroutine waterCannonCoroutine;

    //SpawnBarrel
    BarrelSpawner _barrelSpawn;
    Transform _barrelSpawnTransform;

    //Sword
    SwordTemplate _sword;
    Transform _swordTransform;
    [SerializeField] GameObject SwordModel;

    //for Weapon Switching, 0 is Gun and 1 is Sword
    int weapon = 0;

    private void Move(InputAction.CallbackContext context)
    {
        //Update movement direction
        _movementInput = context.ReadValue<Vector2>();
        _Movement.x = _movementInput.x;
        _Movement.z = _movementInput.y;
        //For animation switching
        bool runing = _movementInput.x != 0 || _movementInput.y != 0;
        if (_isRunning ^ runing)
        {
            _isRunning = runing;
            PlayerAnimator.SetBool(_isRunningHash, runing);
        }
    }
    private void Aim()
    {
        var (success, posn) = GetMouseRay();
        if (success && !_isDead)
        {
            posn.y += 0.5f;
            Vector3 dir = transform.position - posn;
            _gunTransform.forward = dir;
            dir.y = 0;
            transform.forward = dir;
            float angle = Vector3.Angle(dir.normalized, _Movement);
            Vector3 moveOffset = Quaternion.Euler(0, angle + 180, 0) * new Vector3(0,0,1);
            PlayerAnimator.SetFloat(_dirXHash, moveOffset.x);
            PlayerAnimator.SetFloat(_dirZHash, moveOffset.z);
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

    private void playerDeath()
    {
        _playerControls.CharacterControls.Disable();
        _isDead = true;
        PlayerAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        PlayerAnimator.SetBool("isDead", true);
    }

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _characterController = GetComponent<CharacterController>();
        _isRunningHash = Animator.StringToHash("isRunning");
        _dirXHash = Animator.StringToHash("DirectionX");
        _dirZHash = Animator.StringToHash("DirectionZ");
        _gunTransform = transform.Find("Gun");
        _gun = _gunTransform.GetComponent<GunTemplate>();
        _swordTransform = transform.Find("Sword");
        _sword = _swordTransform.GetComponent<SwordTemplate>();
        _barrelSpawnTransform = transform.Find("BarrelSpawn");
        _barrelSpawn = _barrelSpawnTransform.GetComponent<BarrelSpawner>();


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
                //should be replaced with a return from the sword if the attack was succesfull.
                //maybe give the Attack() function a return type to determine if a strike is executed?
                bool _isStriking= true;
                _sword.Attack();
                if (_isStriking) { PlayerAnimator.SetTrigger("Attack"); }
            }

        };
        _playerControls.CharacterControls.SwitchWeapon.started += context =>
        {

            if (weapon == 0)
            {
                weapon = 1;
                GunModel.GetComponent<MeshRenderer>().enabled = false;
                SwordModel.GetComponent<MeshRenderer>().enabled = true;
                AimRig.weight= 0f;
            }
            else if (weapon == 1)
            {
                weapon = 0;
                GunModel.GetComponent<MeshRenderer>().enabled = true;
                SwordModel.GetComponent<MeshRenderer>().enabled = false;
                AimRig.weight = 1f;
            }
        };
        _playerControls.CharacterControls.WaterCannon.started += context =>
        {
            if(weapon == 0) StartWaterCannon();
        };
        _playerControls.CharacterControls.WaterCannon.canceled += context =>
        {
            StopWaterCannon();
        };
        _playerControls.CharacterControls.Invisible.started += context =>
        {
            StartCoroutine(makeInvisible());
        };
        _playerControls.CharacterControls.BarrelSpawner.started += context =>
        {
            _barrelSpawn.SpawnBarrel();
        };
    }
    private void Start()
    {
        GameController.instance.PlayerDeath.AddListener(playerDeath);
    }

    private void OnEnable()
    {
        _playerControls.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.CharacterControls.Disable();
    }
    private void OnDestroy()
    {
        GameController.instance.PlayerDeath.RemoveListener(playerDeath);
    }
    private void Update()
    {
        Aim();
        _characterController.SimpleMove(_Movement * movementSpeed);

    }

    public IEnumerator makeInvisible()
    {
        Material[] originalArr = new Material[PlayerRenderer.materials.Length];
        Array.Copy(PlayerRenderer.materials, originalArr, PlayerRenderer.materials.Length);
        Material[] invisibleArr = new Material[PlayerRenderer.materials.Length];
        for (int i = 0; i < PlayerRenderer.materials.Length; ++i)
        {
            invisibleArr[i] = invisibleMaterial;
        }
        PlayerRenderer.materials = invisibleArr;

        invisible = true;
        yield return new WaitForSeconds(invisibleTime);
        invisible = false;
        
        PlayerRenderer.materials = originalArr;
    }


    void StartWaterCannon()
    {
        waterCannonCoroutine = StartCoroutine(_gun.FireWaterCannon());
    }
    void StopWaterCannon()
    {
        if(waterCannonCoroutine != null)
        {
            StopCoroutine(waterCannonCoroutine);
        }
    }
}

