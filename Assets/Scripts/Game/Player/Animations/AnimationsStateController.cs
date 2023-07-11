using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

namespace Humanoid
{
    public enum EPlayerAnimation
    {
        JumpRifle,
        Fire
    }
    
    public class AnimationsStateController : MonoBehaviour
    {
        [Inject] private IPlayerValues playerValues;
        
        private Animator animator;
        private readonly Dictionary<EPlayerAnimation, int> animationsHash = new();
        private int isRifleRun;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            isRifleRun = Animator.StringToHash("IsRifleRunning");
            FillAnimationHashes();
        }

        private void Start()
        {
            playerValues.PlayerState
                .Subscribe(ChangeState)
                .AddTo(this);
            playerValues.PlayAnimation
                .Subscribe(PlayAnimation)
                .AddTo(this);
        }

        private void ChangeState(EPlayerState playerState)
        {
            switch (playerState)
            {
                case EPlayerState.RunRifle:
                    animator.SetBool(isRifleRun, true);
                    break;
                case EPlayerState.IdleRifle:
                    animator.SetBool(isRifleRun, false);
                    break;
            }
        }

        private void PlayAnimation(EPlayerAnimation animationName)
        {
            if (animationsHash.TryGetValue(animationName, out var value))
            {
                animator.SetTrigger(value);
            }
            else Debug.Log("Animation not found");
        }

        private void FillAnimationHashes()
        {
            animationsHash.Add(EPlayerAnimation.JumpRifle, Animator.StringToHash("IsRifleJump"));
            animationsHash.Add(EPlayerAnimation.Fire, Animator.StringToHash("IsRifleFire"));
        }
    }
}