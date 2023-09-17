using System;
using CombatSystem;
using EnemyUnit;
using NewBattleSystem;
using UnityEngine;

namespace NewBattleSystem.Units.Core
{
    public class AttackAction : MonoBehaviour
    {
        public event Action<IDamageable> OnComplete;

        private readonly IEnemyAnimationController _animation;
        private readonly EnemyModel _model;
        private readonly EnemyView _view;
        private IDamageable _currentTarget;

        public AttackAction(
            EnemyModel model,
            EnemyView view,
            IEnemyAnimationController animation)
        {
            _model = model;
            _view = view;
            _animation = animation;
        }

        public void StartAction(IDamageable target)
        {
            _currentTarget = target;
            
            _animation.ActionMoment += OnActionMoment;
            _animation.AnimationComplete += OnAnimationComplete;
            
            _animation.PlayAnimation(AnimationType.Attack);
        }
        
        public void ClearTarget()
        {
            _currentTarget = null;
        }

        private void OnActionMoment()
        {
            if (_currentTarget != null)
            {
                _currentTarget.TakeDamage(_model.AttackDamage, _view);
            }
            _animation.ActionMoment -= OnActionMoment;
        }

        private void OnAnimationComplete()
        {
            _animation.AnimationComplete -= OnAnimationComplete;
            OnComplete?.Invoke(_currentTarget);
        }
    }
}