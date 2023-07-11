using UniRx;
using UnityEngine;

namespace Humanoid
{
    public enum EPlayerState
    {
        //TODO Rifle States
        Idle,
        IdleRifle,
        Run,
        RunRifle,
        Jump
    }
    
    public class PlayerValues : IPlayerValues
    {
        private ReactiveProperty<float> playerSpeed = new();
        public ReactiveProperty<float> PlayerSpeed => playerSpeed;
        
        private ReactiveProperty<bool> isPlayerGrounded = new(true);
        public ReactiveProperty<bool> IsPlayerGrounded => isPlayerGrounded;
        
        private ReactiveCommand<EPlayerState> playerState = new();
        public ReactiveCommand<EPlayerState> PlayerState => playerState;

        private ReactiveCommand<EPlayerAnimation> playAnimation = new();
        public ReactiveCommand<EPlayerAnimation> PlayAnimation => playAnimation;
        
        public Camera PlayerCamera { get; set; }
    }

    internal interface IPlayerValues
    {
        ReactiveProperty<float> PlayerSpeed { get; }
        ReactiveProperty<bool> IsPlayerGrounded { get; }
        ReactiveCommand<EPlayerState> PlayerState { get; }
        ReactiveCommand<EPlayerAnimation> PlayAnimation { get; }
        Camera PlayerCamera { get; set; }
    }
}