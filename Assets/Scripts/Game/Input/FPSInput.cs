using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Humanoid.Input
{
    [RequireComponent(typeof(CharacterController))]
    [AddComponentMenu("Control script/FPS Input")]
    public class FPSInput : MonoBehaviour
    {
        [SerializeField] private float speed = 3f;
        [SerializeField] private float gravity = -9.8f;
    
        private CharacterController characterController;
        
        void Start()
        {
            characterController = GetComponent<CharacterController>();
        }
        
        void Update()
        {
            Movement();
        }

        private void Movement()
        {
            /*
            var deltaX = Input.GetAxis("Horizontal") * speed;
            var deltaY = Input.GetAxis("Vertical") * speed;
            var movement = new Vector3(deltaX, 0, deltaY);
            movement = Vector3.ClampMagnitude(movement, speed);
            movement.y = gravity;
            movement *= Time.deltaTime;
            movement = transform.TransformDirection(movement);
            characterController.Move(movement);
            */
        }
    }
}