using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonController : MonoBehaviour
{
    CharacterController _controller;
    InputAction _moveAction;
    public Animator anim;

    [Header("Settings")]
    public float speed = 1;
    public Vector2 moveInput;

    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _moveAction = GetComponent<PlayerInput>().actions["Move"];
        anim = GetComponent<Animator>();
    }

    void OnEnable()
    {
        _moveAction.started += GetMoveInput;
        _moveAction.performed += GetMoveInput;
        _moveAction.canceled += GetMoveInput;
    }

    void Start()
    {

    }

    void Update()
    {
        Move();
    }

    void GetMoveInput(InputAction.CallbackContext context)
    {
        Debug.Log("Move");
        moveInput = context.ReadValue<Vector2>();
    }

    void Move()
    {
        Vector3 fb = transform.forward * moveInput.y;
        Vector3 lr = transform.right * moveInput.x;
        _controller.Move((fb + lr) * speed * Time.deltaTime);
    }

}