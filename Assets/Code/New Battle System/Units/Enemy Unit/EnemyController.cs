using System;
using EnemyUnit.Interfaces;
using NewBattleSystem.Units.Core;
using UnityEngine;

namespace EnemyUnit
{
    public class EnemyController : IEnemyController
    {
        private readonly EnemyModel _model;
        private readonly EnemyView _view;
        private readonly EnemyCore _core;
        private readonly EnemyStatesHolder _statesHolder;

        public event Action<int> OnDeath;

        public int Index { get; private set; }

        public EnemyController(
            EnemyModel model, 
            EnemyView view,
            EnemyCore core,
            EnemyStatesHolder statesHolder)
        {
            _model = model;
            _view = view;
            _core = core;
            _statesHolder = statesHolder;

            _view.OnTakeDamage += TakeDamage;
        }

        private void TakeDamage(int damageAmount)
        {
            _model.DecreaseHealth(damageAmount);
            if (_model.CurrentHealth == 0)
            {
                _statesHolder.ChangeState(EnemyStateType.Dying);
                OnDeath?.Invoke(Index);
            }
        }

        public void Init(Vector3 startPosition)
        {
            _view.ChangePosition(startPosition);
            _view.SetActive(true);

            _core.EnableCore(startPosition);

            _statesHolder.Enable(OnStateCompete);

            _statesHolder.ChangeState(EnemyStateType.SearchForTarget);
        }

        private void OnStateCompete(EnemyStateType state)
        {
            switch (state)
            {
                default:
                    break;
                case EnemyStateType.Idle:
                    {
                        _statesHolder.ChangeState(EnemyStateType.Move);
                        break;
                    }
                case EnemyStateType.Move:
                    {
                        _statesHolder.ChangeState(EnemyStateType.Idle);
                        break;
                    }
                case EnemyStateType.SearchForTarget:
                    {
                        var targetPos = _core.FindTarget.TargetFindPosition;
                        _core.Target.ChangePosition(targetPos);
                        _statesHolder.ChangeState(EnemyStateType.Move);
                        break;
                    }
            }
        }

        public void OnUpdate(float deltaTime)
        {
            _statesHolder.OnUpdate(deltaTime);
        }

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            _statesHolder.OnFixedUpdate(fixedDeltaTime);
        }

        public void SetIndex(int index)
            => Index = index;

        public EnemyType GetEnemyType() => _model.Type;

        public void Dispose()
        {
            Disable();

            _statesHolder?.Dispose();
            _core?.Dispose();
        }

        public void Disable()
        {
            _view.SetActive(false);

            _core.DisableCore();

            _statesHolder.Disable();
        }

    }
}
