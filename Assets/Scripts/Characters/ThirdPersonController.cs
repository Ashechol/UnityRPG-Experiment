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
    GameObject _mainCamera;
    float _targetRotation;
    float _rotationVelocity;

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
    public float pitchMax = 70;
    public float pitchMin = -30;
    float _cameraPitch;
    float _cameraYaw;

    void Awake()
    {
        _input = GetComponent<GameInput>();
        _controller = GetComponent<CharacterController>();
        _anim = GetComponent<Animator>();
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Start()
    {
        _cameraYaw = cameraTarget.transform.rotation.eulerAngles.y;
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
        _cameraYaw += _input.lookInput.x;
        _cameraPitch += _input.lookInput.y;
        _cameraYaw = ClampAngle(_cameraYaw, float.MinValue, float.MaxValue);
        _cameraPitch = ClampAngle(_cameraPitch, pitchMin, pitchMax);

        cameraTarget.transform.rotation = Quaternion.Euler(_cameraPitch, _cameraYaw, 0.0f);
    }

    void Move()
    {
        float targetSpeed;
        Vector3 direction = new Vector3(_input.moveInput.x, 0.0f, _input.moveInput.y);

        if (direction == Vector3.zero)
            targetSpeed = 0;
        else
        {
            _targetRotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;

            transform.rotation = Quaternion.Euler(0.0f, _targetRotation, 0.0f);

            targetSpeed = speed;
        }

        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

        _controller.Move(targetDirection.normalized * targetSpeed * Time.deltaTime);
    }

    void GroundedCheck()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y + groundedOffset, transform.position.z);
        grounded = Physics.CheckSphere(spherePosition, groundedRadius, groundedLayer, QueryTriggerInteraction.Ignore);
    }

    float ClampAngle(float angle, float min, float max)
    {
        if (angle > 360f) angle -= 360;
        if (angle < -360f) angle += 360;

        return Mathf.Clamp(angle, min, max);
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
