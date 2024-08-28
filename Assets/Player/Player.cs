using System;
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
    private Animator _animator;
    private ViewCamera _viewCamera;

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
        _characterController.Move(new Vector3(_moveInput.x,0,_moveInput.y) * speed * Time.deltaTime);
    }
}
