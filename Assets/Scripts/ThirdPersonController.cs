using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonController : MonoBehaviour
{
    CharacterController characterController;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    
}