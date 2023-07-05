using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UniRx;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.LowLevel;
using Zenject;
using Touch = UnityEngine.Touch;

namespace Humanoid.Input
{
    public class Movement : MonoBehaviour
    {
        [Inject] private IPlayerParams playerParams;
        
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private InputActionReference movement;
        [SerializeField] private InputActionReference touchInput;
        [SerializeField] private InputActionReference touchPress;
        
        private void Start()
        {
            playerParams.PlayerSpeed.Value = moveSpeed;
            Observable.EveryUpdate()
                .Where(_ => movement.action.IsPressed())
                .Select(_ => movement.action.ReadValue<Vector2>())
                .Subscribe(MovementUpdate)
                .AddTo(this);
        }

        private void OnEnable()
        {
            touchPress.action.started += OnFingerDown;
            touchPress.action.canceled += OnFingerUp;
            //movement.action.started += OnFingerDown;
            movement.action.performed += OnFingerMove;
            movement.action.canceled += OnFingerUp;
        }

        private void OnFingerDown(InputAction.CallbackContext finger)
        {
            Debug.Log("Finger pressed at " + touchInput.action.ReadValue<Vector2>());
        }
        
        private void OnFingerMove(InputAction.CallbackContext finger)
        {
            
        }

        private void OnFingerUp(InputAction.CallbackContext finger)
        {
            Debug.Log("Finger ended " + touchInput.action.ReadValue<Vector2>());
        }

        private void MovementUpdate(Vector2 direction)
        {
            MoveObject(direction);
            //RotationObject(direction);
            moveSpeed = playerParams.PlayerSpeed.Value;
        }
        
        private void MoveObject(Vector2 direction)
        {
            var position = transform.position;
            position.x += direction.x * moveSpeed * Time.deltaTime;
            position.z += direction.y * moveSpeed * Time.deltaTime;
            gameObject.transform.position = position;
        }

        private void RotationObject(Vector2 direction)
        {
            var rotationVector = new Vector3 {x = direction.x, z = direction.y};
            var rotation = Quaternion.LookRotation(rotationVector);
            gameObject.transform.rotation = rotation;
        }

        private void OnDisable()
        {
        }
    }
}