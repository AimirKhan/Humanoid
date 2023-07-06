using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

namespace Humanoid.Input
{
    public class TouchController : MonoBehaviour, ITouchController
    {
        public Action<Finger> HandleFingerDown { get; set; }
        public Action<Finger> HandleFingerMove { get; set; }
        public Action<Finger> HandleFingerUp { get; set; }

        private void OnEnable()
        {
            EnhancedTouchSupport.Enable();
            ETouch.Touch.onFingerDown += HandleFingerDown;
            ETouch.Touch.onFingerMove += HandleFingerMove;
            ETouch.Touch.onFingerUp += HandleFingerUp;
        }
        
        private void OnDisable()
        {
            EnhancedTouchSupport.Disable();
            ETouch.Touch.onFingerDown += HandleFingerDown;
            ETouch.Touch.onFingerMove += HandleFingerMove;
            ETouch.Touch.onFingerUp += HandleFingerUp;
        }
    }

    interface ITouchController
    {
        public Action<Finger> HandleFingerDown { get; set; }
        public Action<Finger> HandleFingerMove { get; set; }
        public Action<Finger> HandleFingerUp { get; set; }
    }
}