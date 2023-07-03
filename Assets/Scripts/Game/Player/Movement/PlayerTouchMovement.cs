using System;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

namespace Humanoid.Input
{
    public class PlayerTouchMovement : MonoBehaviour
    {
        [SerializeField] private float playerSpeed = 3f;
        [SerializeField] private float joystickSize = 256;
        [SerializeField] private FloatingJoystick joystick;

        private Finger movementFinger;
        private Vector2 movementAmount;
        private Vector3 joyCenter;
        private CharacterController characterController;
        private float gravity = -9.8f;
        private bool isMoving;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
        }

        private void Start()
        {
            Observable.EveryUpdate() // Update stream
                .Where(_ => isMoving) // Filter on press any Key
                .Subscribe(_ => Move())
                .AddTo(this); // Link subscribe to GameObject
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
            if (movementFinger == null && touchPos.x <= Screen.width / 2)
            {
                movementFinger = touch;
                movementAmount = Vector2.zero;
                joystick.gameObject.SetActive(true);
                joystick.RectTransform.sizeDelta = Vector2.one * joystickSize;
                var knobPos = ClampStartPosition(touchPos);
                joystick.RectTransform.anchoredPosition = knobPos;
                joyCenter = knobPos;
                isMoving = true;
            }
        }
        
        private void OnFingerMove(Finger touch)
        {
            if (touch == movementFinger)
            {
                var offset = (Vector3)touch.screenPosition - joyCenter;
                joystick.knob.position = joyCenter + Vector3.ClampMagnitude(offset, joystickSize / 2);
                
                movementAmount = offset.normalized;
                Debug.Log("Player vector is " + movementAmount);
                //TODO float movement
            }
        }

        private void OnFingerUp(Finger touch)
        {
            if (touch == movementFinger)
            {
                movementFinger = null;
                joystick.knob.anchoredPosition = Vector3.zero;
                joystick.gameObject.SetActive(false);
                movementAmount = Vector2.zero;
                isMoving = false;
            }
        }

        private Vector2 ClampStartPosition(Vector2 touchPos)
        {
            var joySize = joystickSize / 2;
            var joyYTopClamp = Screen.height - joySize;
            
            if (touchPos.x < joySize)
                touchPos.x = joySize;
            
            if (touchPos.y < joySize)
                touchPos.y = joySize;
            else if (touchPos.y > joyYTopClamp)
                touchPos.y = joyYTopClamp;
            
            return touchPos;
        }

        private void Move()
        {
            var deltaX = movementAmount.x * playerSpeed;
            var deltaY = movementAmount.y * playerSpeed;
            var movement = new Vector3(deltaX, 0, deltaY);
            movement = Vector3.ClampMagnitude(movement, playerSpeed);
            movement.y = gravity;
            movement *= Time.deltaTime;
            movement = transform.TransformDirection(movement);
            characterController.Move(movement);
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