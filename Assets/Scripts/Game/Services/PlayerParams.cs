using UniRx;

namespace Humanoid
{
    public enum EPlayerState
    {
        Idle,
        Move,
        Run,
        Jump
    }
    
    public class PlayerValues : IPlayerValues
    {
        private ReactiveProperty<float> playerSpeed = new();
        private ReactiveProperty<bool> isPlayerGrounded = new(true);
        
        public ReactiveProperty<float> PlayerSpeed => playerSpeed;
        public ReactiveProperty<bool> IsPlayerGrounded => isPlayerGrounded;


        private ReactiveProperty<EPlayerState> playerState = new(EPlayerState.Idle);
        public ReactiveProperty<EPlayerState> PlayerState => playerState;
    }

    public interface IPlayerValues
    {
        ReactiveProperty<float> PlayerSpeed { get; }
        ReactiveProperty<bool> IsPlayerGrounded { get; }
        ReactiveProperty<EPlayerState> PlayerState { get; }
    }
}