using System;
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

    void Awake()
    {
        _input = GetComponent<PlayerInput>();
        lookAction = _input.actions["Look"];
    }

    void OnEnable()
    {
        _input.onActionTriggered += GetMoveInput;
        _input.onActionTriggered += GetLookInput;
        _input.onActionTriggered += GetNormalAttackInput;

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

        normalAttack = context.action.name == "NormalAttack" ? true : false;
    }

    void GetChargeAttackInput(InputAction.CallbackContext context)
    {
        normalAttack = context.action.name == "ChargeAttack" ? true : false;
    }
}
