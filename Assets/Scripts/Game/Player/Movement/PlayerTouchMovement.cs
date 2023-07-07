using System;
using UniRx;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Zenject;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

namespace Humanoid.Input
{
    public class PlayerTouchMovement : MonoBehaviour
    {
        [Inject] private IPlayerValues playerValues;
        [SerializeField] private Transform cameraDirection;
        [SerializeField] private float playerSpeed = 3;
        [SerializeField] private float joystickSize = 256;
        [SerializeField] private FloatingJoystick joystick;

        [Header("Player Grounded")]
        [SerializeField] private bool grounded = true;
        [SerializeField] private float groundedOffset = -0.14f;
        [SerializeField] private LayerMask groundLayers;
        
        private CharacterController characterController;
        private Finger movementFinger;
        private Vector2 movementAmount;
        private Vector3 joyCenter;
        private Vector3 playerVelocity;
        private bool isMoving;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
        }

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            Observable.EveryUpdate() // Update stream
                .Where(_ => isMoving) // Filter on press any Key
                .Subscribe(_ => CharacterUpdate())
                .AddTo(this); // Link subscribe to GameObject

            Observable.EveryUpdate()
                .Where(ctx => playerValues.IsPlayerGrounded.Value != true)
                .Subscribe(ctx => GroundedCheck());
        }

        private void CharacterUpdate()
        {
            Move();
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
                //TODO change normilzed vector
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
            var rotationY = cameraDirection.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0, rotationY, 0);
            
            var move = new Vector3(movementAmount.x, 0, movementAmount.y);
            move = transform.TransformDirection(move);
            characterController.Move(move * playerSpeed * Time.deltaTime);
            if (move != Vector3.zero)
                gameObject.transform.forward = move;
            GroundedCheck();
        }
        
        private void GroundedCheck()
        {
            var position = transform.position;
            var spherePosition = new Vector3(position.x, position.y - groundedOffset, position.z);
            grounded = Physics.CheckSphere(spherePosition, characterController.radius, groundLayers,
                QueryTriggerInteraction.Ignore);
            playerValues.IsPlayerGrounded.Value = grounded;
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