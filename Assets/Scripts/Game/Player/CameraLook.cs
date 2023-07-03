using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Humanoid.Input
{
    public class CameraLook : MonoBehaviour
    {
        [SerializeField] private float horSensitivity = 3f;
        [SerializeField] private float verSensitivity = 3f;
        [SerializeField] private float maxVert = 45f;
        [SerializeField] private float minVert = -45f;
        
        private float rotationX = 0;
        
        private void Start()
        {
            var body = GetComponent<Rigidbody>();
            if (body != null) body.freezeRotation = true;
            
            /*
            Observable.EveryUpdate()
                .Where(_ => movement.action.IsPressed())
                .Select(_ => movement.action.ReadValue<Vector2>())
                .Subscribe(MovementUpdate)
                .AddTo(this);
                */
        }
        
        private void RotateMouse()
        {
            /*
            rotationX -= Input.GetAxis("Mouse Y") * verSensitivity;
            rotationX = Mathf.Clamp(rotationX, minVert, maxVert);
            var delta = Input.GetAxis("Mouse X") * horSensitivity;
            var rotationY = transform.localEulerAngles.y + delta;
    
            transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
            */
        }
    }
}