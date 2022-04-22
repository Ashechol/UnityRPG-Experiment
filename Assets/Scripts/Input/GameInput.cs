using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class GameInput : MonoBehaviour
{
    PlayerInput _input;

    public Vector2 moveInput;
    public Vector2 lookInput;

    void Awake()
    {
        _input = GetComponent<PlayerInput>();
    }

    void OnEnable()
    {
        _input.onActionTriggered += GetMoveInput;
        _input.onActionTriggered += GetLookInput;
    }

    void OnDisable()
    {
        _input.onActionTriggered -= GetMoveInput;
        _input.onActionTriggered -= GetLookInput;
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
}
