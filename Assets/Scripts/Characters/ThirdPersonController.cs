using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(GameInput))]
public class ThirdPersonController : MonoBehaviour
{
    CharacterController _controller;
    GameInput _input;
    Animator _anim;

    [Header("Basic Settings")]
    public float speed = 3;
    public float gravity = -15;

    void Awake()
    {
        _input = GetComponent<GameInput>();
        _controller = GetComponent<CharacterController>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 direction = transform.forward * _input.moveInput.y + transform.right * _input.moveInput.x;
        Vector3 vertical = Vector3.up * gravity * Time.deltaTime;

        _controller.Move(direction * speed * Time.deltaTime + vertical);

        _anim.SetFloat("speed", _controller.velocity.magnitude);
    }

    //TODO: 地面监测
    //TODO: 转向
    //TODO: 鼠标控制
}
