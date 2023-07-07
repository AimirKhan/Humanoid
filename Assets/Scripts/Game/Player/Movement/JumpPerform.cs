using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Humanoid.Input
{
    public class JumpPerform : MonoBehaviour
    {
        [Inject] private IGlobalValues globalValues;
        [Inject] private IPlayerValues playerValues;
        [SerializeField] private float jumpHeight = 1;
        [SerializeField] private InputActionReference playerJump;

        private CharacterController characterController;
        private Vector3 playerVelocity;
        private bool isPlayerGrounded = true;
        private float gravity = -9.8f;

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
            // enable gravity if player is not on the ground
            playerValues.IsPlayerGrounded
                .Subscribe(ctx => isPlayerGrounded = ctx)
                .AddTo(this);
            Observable.EveryUpdate()
                .Where(_ => !isPlayerGrounded)
                .Subscribe(x => MovePlayer())
                .AddTo(this);
        }

        private void OnEnable()
        {
            playerJump.action.performed += PerformJump;
        }

        private void PerformJump(InputAction.CallbackContext obj)
        {
            if (isPlayerGrounded)
            {
                Debug.Log("Player jump started!");
                playerVelocity.y = Mathf.Sqrt(jumpHeight * -3f * gravity);
                playerValues.IsPlayerGrounded.Value = false;
                MovePlayer();
            }
        }

        private void MovePlayer()
        {
            Debug.Log("Player is grounded " + isPlayerGrounded);
            playerVelocity.y += gravity * Time.deltaTime;
            characterController.Move(playerVelocity * Time.deltaTime);
        }

        private void OnDisable()
        {
            playerJump.action.performed -= PerformJump;
        }
    }
}