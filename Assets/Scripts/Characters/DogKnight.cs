using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerStats))]
[RequireComponent(typeof(GameInput))]
public class DogKnight : MonoBehaviour
{
    [Header("Basic Settings")]
    public float speed = 3.5f;
    public float rotateSmoothTime;
    public float jumpHeight = 1.0f;

    [Header("Grounded Settings")]
    public bool grounded;
    public float gravity = -15;
    public float checkRadius;
    public float checkOffset;
    public LayerMask checkLayermask;

    [Header("Camera Settings")]
    public GameObject cameraTarget;
    public float pitchMax = 70;
    public float pitchMin = -30;

    CharacterController _controller;
    Animator _anim;
    GameInput _input;
    // GameObject _mainCamera;
    PlayerStats stats;

    // camera
    float _cameraPitch;
    float _cameraYaw;

    // move
    float _targetRotation;
    float _rotateVelocity; // for smooth rotation
    float _horizontalVelocity;

    // jump
    float _verticalVelocity;

    // combat
    float _attackDelta;

    // animator
    bool _animDead;
    bool _animHit;
    float _animSpeed;

    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _anim = GetComponent<Animator>();
        _input = GetComponent<GameInput>();
        // _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        stats = GetComponent<PlayerStats>();
    }

    void Start()
    {
        _attackDelta = stats.attackData.attackRate;
    }

    void Update()
    {
        UpdateAnimation();
        GrounedCheck();
        JumpAndGravity();
        Move();
        Combat();
    }

    void LateUpdate()
    {
        CameraControl();
    }

    void CameraControl()
    {
        _cameraPitch += _input.lookInput.y;
        _cameraPitch = ClampAngle(_cameraPitch, pitchMin, pitchMax);
        _cameraYaw += _input.lookInput.x;
        _cameraYaw = ClampAngle(_cameraYaw, float.MinValue, float.MaxValue);

        cameraTarget.transform.rotation = Quaternion.Euler(_cameraPitch, _cameraYaw, 0.0f);
    }

    void Move()
    {
        float currentSpeed;

        // gravity


        // rotation
        if (_input.moveInput == Vector2.zero || _input.isStopped)
            currentSpeed = 0.0f;
        else
        {
            currentSpeed = speed;

            Vector3 direction = new Vector3(_input.moveInput.x, 0.0f, _input.moveInput.y);

            // targetRotation = direction's rotation + camera's rotation (eulerAngle.y)
            _targetRotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTarget.transform.eulerAngles.y;

            // Smooth the rotation
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotateVelocity, rotateSmoothTime);

            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        // CharacterController.Move is based on global coordinate, use Vector3.forward
        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

        _controller.Move(targetDirection * currentSpeed * Time.deltaTime + Vector3.up * _verticalVelocity);

        _animSpeed = currentSpeed;
    }

    //TODO: JUMP!
    void JumpAndGravity()
    {
        if (grounded)
        {
            _verticalVelocity = 0.0f;
        }
        else
        {
            _verticalVelocity = gravity;
        }
    }

    void Combat()
    {
        _attackDelta -= Time.deltaTime;
        if (_input.normalAttack && _attackDelta <= 0)
        {
            _anim.SetTrigger("attack");
            _attackDelta = stats.attackData.attackRate;
        }
        if (_input.chargeAttack && _attackDelta <= 0)
        {
            _anim.SetTrigger("attack");
            _anim.SetBool("critical", true);
            _attackDelta = stats.attackData.attackRate;
        }
        else
            _anim.SetBool("critical", false);
    }

    void GrounedCheck()
    {
        Vector3 position = new Vector3(transform.position.x, transform.position.y + checkOffset, transform.position.z);
        grounded = Physics.CheckSphere(position, checkRadius, checkLayermask, QueryTriggerInteraction.Ignore);
    }

    void UpdateAnimation()
    {
        _anim.SetFloat("speed", _animSpeed);
    }

    float ClampAngle(float angle, float min, float max)
    {
        if (angle > 360f) angle -= 360f;
        if (angle < -360f) angle += 360f;

        return Mathf.Clamp(angle, min, max);
    }

    void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.3f);
        Color transparentRed = new Color(1.0f, 0, 0, 0.3f);

        Gizmos.color = grounded ? transparentGreen : transparentRed;

        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y
        + checkOffset, transform.position.z), checkRadius);
    }

}
