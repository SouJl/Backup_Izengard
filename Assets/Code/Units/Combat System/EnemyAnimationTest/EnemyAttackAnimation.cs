using System;
using UnityEngine;
using Wave;


namespace CombatSystem
{
    public class EnemyAttackAnimation : IEnemyAnimation
    {
        private const float DASH_DISTANCE = 0.000000001f;  //fix for jiggling
        private const float DASH_SPEED = 0.00000001f;

        public event Action ActionMoment;
        public event Action AnimationComplete;

        private readonly Enemy _unit;
        private Vector3 _startPosition;
        private float _currentDashPosition;
        private bool _isStartMoving;


        public EnemyAttackAnimation(Enemy unit)
        {
            _unit = unit;
        }

        public void OnFixedUpdate(float deltaTime)
        {
            if (_isStartMoving)
            {
                _currentDashPosition += DASH_SPEED * deltaTime;
                _unit.RootGameObject.transform.Translate(Vector3.forward * DASH_SPEED * deltaTime);
                if (_currentDashPosition > DASH_DISTANCE)
                {
                    _isStartMoving = false;
                    ActionMoment?.Invoke();
                }
            }
            else
            {
                _currentDashPosition -= DASH_SPEED * deltaTime;
                if (_currentDashPosition < 0)
                {
                    _unit.RootGameObject.transform.localPosition = _startPosition;
                    AnimationComplete?.Invoke();
                }
                _unit.RootGameObject.transform.Translate(Vector3.back * DASH_SPEED * deltaTime);
            }
        }

        public void PlayAnimation()
        {
            _startPosition = _unit.RootGameObject.transform.localPosition;
            _currentDashPosition = 0;
            _isStartMoving = true;
        }
    }
}