using UnityEngine;
using UnityEngine.InputSystem;
using UniRx;
using Zenject;

namespace Humanoid.Input
{
    public class Movement : MonoBehaviour
    {
        [Inject] private IPlayerParams playerParams;
        
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private InputActionReference movement;
        
        private void Start()
        {
            playerParams.PlayerSpeed.Value = moveSpeed;
            Observable.EveryUpdate()
                .Where(_ => movement.action.IsPressed())
                .Select(_ => movement.action.ReadValue<Vector2>())
                .Subscribe(MovementUpdate)
                .AddTo(this);
        }

        private void MovementUpdate(Vector2 direction)
        {
            MoveObject(direction);
            RotationObject(direction);
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
    }
}