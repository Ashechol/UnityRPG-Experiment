using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class GameInput : MonoBehaviour
{
    PlayerInput _input;
    InputAction lookAction;

    public Vector2 moveInput;
    public Vector2 lookInput;

    [HideInInspector]
    public bool normalAttack;
    public bool chargeAttack;
    public bool isStopped;

    void Awake()
    {
        _input = GetComponent<PlayerInput>();
        lookAction = _input.actions["Look"];
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnEnable()
    {
        _input.onActionTriggered += GetMoveInput;
        _input.onActionTriggered += GetLookInput;
        _input.onActionTriggered += GetNormalAttackInput;
        _input.onActionTriggered += GetChargeAttackInput;

        // _input.actions["ChargeAttack"].performed += GetChargeAttackInput;
        // _input.actions["ChargeAttack"].canceled += GetChargeAttackInput;
    }

    void Update()
    {

    }

    void OnDisable()
    {
        _input.onActionTriggered -= GetMoveInput;
        _input.onActionTriggered -= GetLookInput;
        _input.onActionTriggered -= GetNormalAttackInput;
    }

    void GetMoveInput(InputAction.CallbackContext context)
    {
        if (context.action.name == "Move")
            moveInput = context.ReadValue<Vector2>();
    }

    void GetLookInput(InputAction.CallbackContext context)
    {

        if (context.action.name == "Look")
            lookInput = context.ReadValue<Vector2>();
    }

    void GetNormalAttackInput(InputAction.CallbackContext context)
    {
        if (context.action.name == "NormalAttack")
        {
            if (context.performed)
                normalAttack = true;
            else
                normalAttack = false;
        }
    }

    void GetChargeAttackInput(InputAction.CallbackContext context)
    {
        if (context.action.name == "ChargeAttack")
        {
            if (context.performed)
                chargeAttack = true;
            else
                chargeAttack = false;
        }
    }
}
