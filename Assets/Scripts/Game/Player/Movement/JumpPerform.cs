using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Catventure.Input
{
    public class JumpPerform : MonoBehaviour
    {
        [SerializeField] private float jumpHeight = 4;
        [SerializeField] private InputActionReference playerJump;

        private CharacterController characterController;
        private float gravity = -9.8f;
        private float gravityScale = 1;
        private float velocity;
        
        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
        }

        private void Start()
        {
            // enable gravity if player is not on the ground
            Observable.EveryUpdate()
                .Where(_ => !characterController.isGrounded)
                .Subscribe(x => MovePlayer())
                .AddTo(this);
        }

        private void OnEnable()
        {
            playerJump.action.performed += PerformJump;
        }

        private void PerformJump(InputAction.CallbackContext obj)
        {
            if (characterController.isGrounded)
            {
                Debug.Log("Player jumped!");
                velocity = Mathf.Sqrt(jumpHeight * -2f * (gravity * gravityScale));
                MovePlayer();
            }
        }

        void MovePlayer()
        {
            velocity += gravity * gravityScale * Time.deltaTime;
            characterController.Move((Vector3.up * velocity * Time.deltaTime));
        }
        
        private void OnDisable()
        {
            playerJump.action.performed -= PerformJump;
        }
    }
}