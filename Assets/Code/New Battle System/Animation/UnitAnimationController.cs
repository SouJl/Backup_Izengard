using System;
using UnityEngine;

namespace NewBattleSystem.Animation
{
    public class UnitAnimationController : IAnimationController<UnitAnimationType>
    {
        public bool IsAnimationPlaying => throw new NotImplementedException();

        public event Action AnimationComplete;

        private UnitAnimationType _currentAnimation;

        public UnitAnimationController() 
        {

        }

        public void PlayAnimation(UnitAnimationType animationType)
        {
            Debug.Log($"Start playing {animationType}");
            _currentAnimation = animationType;
        }

        public void StopAnimation()
{
            Debug.Log($"Stop playing {_currentAnimation}");
            AnimationComplete?.Invoke();
        }

        public void OnUpdate(float deltaTime) { }

        public void OnFixedUpdate(float fixedDeltaTime) { }

        public void Dispose() { }
    }
}
