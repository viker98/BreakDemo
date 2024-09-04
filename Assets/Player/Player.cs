using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(CharacterController))]
[RequireComponent(typeof(Animator))]

public class Player : MonoBehaviour
{
    [SerializeField] private GamePlayWidget gameplayWidgetPrefab;
    [SerializeField] private float speed = 10f;
    [SerializeField] float bodyRotationSpeed = 10f;
    [SerializeField] ViewCamera viewCameraPrefab;
    
    private GamePlayWidget _gameplayWidget;
    private CharacterController _characterController;
    private ViewCamera _viewCamera;

    private Animator _animator;
    private Vector2 _moveInput;
    private Vector2 _aimInput;

    static int animFwdId = Animator.StringToHash("Forward Amount");
    static int animRightId = Animator.StringToHash("RightAmount");
    static int animTurnId = Animator.StringToHash("Turn Amount");

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _gameplayWidget = Instantiate(gameplayWidgetPrefab);
        _gameplayWidget.MoveStick.OnInputUpdated += MoveInputUpdated;
        _gameplayWidget.AimStick.OnInputUpdated += AimInputUpdated;
        _viewCamera = Instantiate(viewCameraPrefab);
        _viewCamera.SetFollorParent(transform);
    }

    private void AimInputUpdated(Vector2 inputVal)
    {
        _aimInput = inputVal;
    }

    private void MoveInputUpdated(Vector2 inputVal)
    {
        _moveInput = inputVal;
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

        _animator.SetFloat(animTurnId, angleDelta / Time.deltaTime);


        float animFwdAmt = Vector3.Dot(moveDir, transform.forward);
        float animRightAmt = Vector3.Dot(moveDir,transform.right);
        
        
        _animator.SetFloat(animFwdId, animFwdAmt);
        _animator.SetFloat(animRightId, animRightAmt);

    }
}
