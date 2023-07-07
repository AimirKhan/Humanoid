using Humanoid;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Humanoid.Input
{
    public class RunPerform : MonoBehaviour
    {
        [Inject] private IPlayerValues playerValues;
    
        [SerializeField] private float runModifier = 1.2f;
        [SerializeField] private InputActionReference playerRun;

        private void OnEnable()
        {
            playerRun.action.performed += (run => playerValues.PlayerSpeed.Value *= runModifier);
            playerRun.action.canceled += (run => playerValues.PlayerSpeed.Value /= runModifier);
        }
        private void OnDisable()
        {
            playerRun.action.performed -= (run => playerValues.PlayerSpeed.Value *= runModifier);
            playerRun.action.canceled -= (run => playerValues.PlayerSpeed.Value /= runModifier);
        }
    }
}