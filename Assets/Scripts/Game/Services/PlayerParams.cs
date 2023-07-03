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
    
    public class PlayerParams : IPlayerParams
    {
        private ReactiveProperty<float> playerSpeed = new();
        public ReactiveProperty<float> PlayerSpeed => playerSpeed;


        private ReactiveProperty<EPlayerState> playerState = new(EPlayerState.Idle);
        public ReactiveProperty<EPlayerState> PlayerState => playerState;
    }

    public interface IPlayerParams
    {
        ReactiveProperty<float> PlayerSpeed { get; }
        ReactiveProperty<EPlayerState> PlayerState { get; }
    }
}