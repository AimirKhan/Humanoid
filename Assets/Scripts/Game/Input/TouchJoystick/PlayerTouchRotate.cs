using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

namespace Humanoid.Input
{
    public class PlayerTouchRotate : MonoBehaviour
    {
        [SerializeField] private Transform cameraTarget;
        [SerializeField] private float horSensitivity = 1f;
        [SerializeField] private float verSensitivity = 1f;
        
        [SerializeField] private float maxVert = 20f;
        [SerializeField] private float minVert = -30f;

        private float rotationX;
        private Finger movementFinger;
        private Vector2 lastFingerPos;
        private Vector2 movementAmount;
        private bool isMoving;
        
        void Start()
        {
            var body = GetComponent<Rigidbody>();
            if (body != null) body.freezeRotation = true;
        }
        
        private void OnEnable()
        {
            EnhancedTouchSupport.Enable();
            ETouch.Touch.onFingerDown += OnFingerDown;
            ETouch.Touch.onFingerMove += OnFingerMove;
            ETouch.Touch.onFingerUp += OnFingerUp;
        }

        private void OnFingerDown(Finger touch)
        {
            var touchPos = touch.screenPosition;
            if (movementFinger == null && touchPos.x >= Screen.width / 2)
            {
                movementFinger = touch;
                lastFingerPos = touch.currentTouch.screenPosition;
                movementAmount = Vector2.zero;
                isMoving = true;
            }
        }

        private void OnFingerMove(Finger touch)
        {
            var touchPos = touch.screenPosition;
            if (touch == movementFinger)
            {
                RotateObject(touch);
            }
        }

        private void OnFingerUp(Finger touch)
        {
            if (touch == movementFinger)
            {
                movementFinger = null;
                movementAmount = Vector2.zero;
                isMoving = false;
            }
        }

        private void RotateObject(Finger finger)
        {
            var fingerPos = lastFingerPos - finger.currentTouch.screenPosition;
            // Player horizontal rotation
            rotationX += fingerPos.y * horSensitivity;
            rotationX = Mathf.Clamp(rotationX, minVert, maxVert);
            var delta = -fingerPos.x * verSensitivity;
            var rotationY = transform.localEulerAngles.y + delta;
            
            transform.localEulerAngles = Vector3.up * rotationY;

            movementAmount = new Vector2(rotationY, rotationX);
            // Camera vertical rotation
            cameraTarget.localEulerAngles = Vector3.right * rotationX;
            
            lastFingerPos = finger.currentTouch.screenPosition;
        }
        
        private void OnDisable()
        {
            EnhancedTouchSupport.Enable();
            ETouch.Touch.onFingerDown -= OnFingerDown;
            ETouch.Touch.onFingerMove -= OnFingerMove;
            ETouch.Touch.onFingerUp -= OnFingerUp;
        }
    }
}