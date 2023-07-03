using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Humanoid.Input
{
    public class TouchJoystickColor : MonoBehaviour
    {
        [SerializeField] private Color joyColor = Color.red;
        [SerializeField] private List<Image> sprites;
        
        private void Start()
        {
            SetColor();
        }

        private void SetColor()
        {
            if (sprites.Count == 0)
                return;
            
            foreach (var sprite in sprites)
            {
                sprite.color = joyColor;
            }
        }
    }
}