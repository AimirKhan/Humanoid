using UnityEngine;

namespace Humanoid.Input
{
    [RequireComponent(typeof(RectTransform))]
    [DisallowMultipleComponent]
    public class FloatingJoystick : MonoBehaviour
    {
        [HideInInspector]
        public RectTransform RectTransform;
        public RectTransform knob;
        
        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
            gameObject.SetActive(false);
        }

        private void OnDisable()
        {
        }
    }
}