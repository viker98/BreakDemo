using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SocketManager))]
[RequireComponent(typeof(InventoryComponent))]
[RequireComponent(typeof(HealthComponent))]
public class Player : MonoBehaviour
{
    [SerializeField] private GamePlayWidget gameplayWidgetPrefab;
    [SerializeField] private float speed = 10f;
    [SerializeField] float bodyRotationSpeed = 10f;
    [SerializeField] ViewCamera viewCameraPrefab;
    [SerializeField] private float animTurnLerpScale = 5f;


    private GamePlayWidget _gameplayWidget;
    private CharacterController _characterController;
    private ViewCamera _viewCamera;
    private InventoryComponent _inventoryComponent;

    private Animator _animator;
    private float _animTurnSpeed;
    private Vector2 _moveInput;
    private Vector2 _aimInput;

    private static readonly int animFwdId = Animator.StringToHash("Forward Amount");
    private static readonly int animRightId = Animator.StringToHash("RightAmount");
    private static readonly int animTurnId = Animator.StringToHash("Turn Amount");
    private static readonly int SwitchWeaponID = Animator.StringToHash("Switch Weapon");
    private static readonly int FireID = Animator.StringToHash("Firing");

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _inventoryComponent = GetComponent<InventoryComponent>();
        _gameplayWidget = Instantiate(gameplayWidgetPrefab);
        _gameplayWidget.MoveStick.OnInputUpdated += MoveInputUpdated;
        _gameplayWidget.AimStick.OnInputUpdated += AimInputUpdated;
        _gameplayWidget.AimStick.OnInputClicked += AimInputClicked;
        _viewCamera = Instantiate(viewCameraPrefab);
        _viewCamera.SetFollorParent(transform);
    }

    private void AimInputClicked(Vector2 inputVal)
    {
        _animator.SetTrigger(SwitchWeaponID);
       // _inventoryComponent.EquipNextWeapon();
    }

    public void WeaponSwitchPoint()
    {
        _inventoryComponent?.EquipNextWeapon();
    }


    private void AimInputUpdated(Vector2 inputVal)
    {
        _aimInput = inputVal;
        _animator.SetBool(FireID, _aimInput != Vector2.zero);
    }

    private void MoveInputUpdated(Vector2 inputVal)
    {
        _moveInput = inputVal;
    }

    public void AttackPoint()
    {
        _inventoryComponent.FireCurrentActiveWeapon();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDir = _viewCamera.InputToWorldDir(_moveInput);
        _characterController.Move(moveDir * (speed * Time.deltaTime));

        Vector3 aimDir = _viewCamera.InputToWorldDir(_aimInput);
        if (aimDir == Vector3.zero)
        {
            aimDir = moveDir;
            _viewCamera.AddYawInput(_moveInput.x);
        }

        float angleDelta = 0f;
        if (aimDir != Vector3.zero)
        {
            Vector3 prevDir = transform.forward;
            Quaternion goatRot = Quaternion.LookRotation(aimDir, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, goatRot, Time.deltaTime * bodyRotationSpeed);
            angleDelta = Vector3.SignedAngle(transform.forward, prevDir, Vector3.up);

        }
            
        _animTurnSpeed = Mathf.Lerp(_animTurnSpeed, angleDelta/Time.deltaTime, Time.deltaTime * animTurnLerpScale);

        _animator.SetFloat(animTurnId, _animTurnSpeed);


        float animFwdAmt = Vector3.Dot(moveDir, transform.forward);
        float animRightAmt = Vector3.Dot(moveDir,transform.right);
        
        
        _animator.SetFloat(animFwdId, animFwdAmt);
        _animator.SetFloat(animRightId, animRightAmt);

    }
}
