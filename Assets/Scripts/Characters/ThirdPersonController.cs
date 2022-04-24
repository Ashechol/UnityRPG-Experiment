using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(GameInput))]
public class ThirdPersonController : MonoBehaviour
{
    CharacterController _controller;
    GameInput _input;
    Animator _anim;
    Vector3 _velocity;

    float _cameraPitch;
    float _cameraYaw;

    [Header("Basic Settings")]
    public float speed = 3;
    public float sensitivity = 1;
    public float gravity = -15;

    [Header("Ground Check")]
    public bool grounded;
    public float groundedRadius;
    public float groundedOffset;
    public LayerMask groundedLayer;

    [Header("Cinemachine")]
    public GameObject cameraTarget;

    void Awake()
    {
        _input = GetComponent<GameInput>();
        _controller = GetComponent<CharacterController>();
        _anim = GetComponent<Animator>();
    }

    void Start()
    {

    }

    void Update()
    {
        GroundedCheck();
        Move();
    }

    void LateUpdate()
    {
        CameraControll();
    }

    void CameraControll()
    {
        _cameraYaw = _input.lookInput.x;
        _cameraPitch = _input.lookInput.y;

        cameraTarget.transform.Rotate(0, _cameraYaw, 0);
    }

    void Move()
    {
        _velocity = transform.forward * _input.moveInput.y * speed;

        if (!grounded)
            _velocity += Vector3.up * gravity;

        _controller.Move(_velocity * Time.deltaTime);
        transform.Rotate(Vector3.up, _input.moveInput.x * sensitivity * Time.deltaTime);

        _anim.SetFloat("speed", _controller.velocity.magnitude);
    }

    void GroundedCheck()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y + groundedOffset, transform.position.z);
        grounded = Physics.CheckSphere(spherePosition, groundedRadius, groundedLayer, QueryTriggerInteraction.Ignore);
    }

    void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (grounded) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;

        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y + groundedOffset, transform.position.z);
        Gizmos.DrawSphere(spherePosition, groundedRadius);
    }

    //TODO: 地面监测
    //TODO: 转向
    //TODO: 鼠标控制
}
