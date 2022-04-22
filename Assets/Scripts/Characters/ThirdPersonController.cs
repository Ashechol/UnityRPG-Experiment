using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonController : MonoBehaviour
{
    CharacterController _controller;
    InputAction _moveAction;
    public InputReader input;
    public Animator anim;

    [Header("Settings")]
    public float speed = 1;
    public float currentSpeed;
    public Vector2 moveInput;

    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _moveAction = GetComponent<PlayerInput>().actions["Move"];
        anim = GetComponent<Animator>();
    }

    void OnEnable()
    {
        // _moveAction.started += GetMoveInputStarted;
        // _moveAction.performed += GetMoveInputPerformed;
        // _moveAction.canceled += GetMoveInputCanceled;

    }

    void Start()
    {

    }

    void Update()
    {

        Move();
    }

    void GetMoveInputStarted(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log("Move Started" + moveInput);
    }

    void GetMoveInputPerformed(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log("Move Performed" + moveInput);
    }

    void GetMoveInputCanceled(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log("Move Canceled" + moveInput);
    }

    void Move()
    {
        Vector3 direction = transform.forward * moveInput.y + transform.right * moveInput.x;
        Vector3 selfDirection = new Vector3(moveInput.x, 0, moveInput.y);
        currentSpeed = new Vector3(_controller.velocity.x, 0, _controller.velocity.y).magnitude;

        _controller.Move(direction * speed * Time.deltaTime);
        // transform.Translate(direction * speed * Time.deltaTime, Space.World);
        // transform.Translate(selfDirection * speed * Time.deltaTime);
        // transform.position += direction * speed * Time.deltaTime;

    }

}