using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(CharacterController))]
[RequireComponent(typeof(Animator))]

public class Player : MonoBehaviour
{
    [SerializeField] private GamePlayWidget gameplayWidgetPrefab;
    [SerializeField] private float speed;
    [SerializeField] ViewCamera viewCameraPrefab;
    private GamePlayWidget _gameplayWidget;
    private CharacterController _characterController;
    private ViewCamera _viewCamera;

    private Animator _animator;
    private Vector2 _moveInput;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _gameplayWidget = Instantiate(gameplayWidgetPrefab);
        _gameplayWidget.MoveStick.OnInputUpdated += InputUpdated;
        _viewCamera = Instantiate(viewCameraPrefab);
        _viewCamera.SetFollorParent(transform);
    }

    private void InputUpdated(Vector2 inputVal)
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
    }
}
