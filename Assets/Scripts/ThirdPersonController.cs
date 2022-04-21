using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonController : MonoBehaviour
{
    CharacterController characterController;
    public InputAction moveAction;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Move()
    {

    }

}