using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Catventure.Input
{
    public class JumpPerform : MonoBehaviour
    {
        [SerializeField] private float jumpForce = 2;
        [SerializeField] private InputActionReference playerJump;

        private Rigidbody _playerRigidbody;
     
        private void Awake()
        {
            _playerRigidbody = GetComponent<Rigidbody>();
        }
    
        private void OnEnable()
        {
            playerJump.action.performed += Jump;
        }

        private void Jump(InputAction.CallbackContext obj)
        {
            
            _playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    
        private void OnDisable()
        {
            playerJump.action.performed -= (jump => _playerRigidbody.AddForce(Vector3.up * jumpForce));
        }
    }
}