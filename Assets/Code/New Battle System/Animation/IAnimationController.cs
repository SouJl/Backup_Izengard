using System;

namespace NewBattleSystem.Animation
{
    public interface IAnimationController<T> : IOnController, IOnUpdate, IOnFixedUpdate, IDisposable
    {
        event Action AnimationComplete;
        bool IsAnimationPlaying { get; }
        void PlayAnimation(T animationType);
        void StopAnimation();
    }
}
