using System;
using System.Collections.Generic;
using EnemyUnit.EnemyStates;
using NewBattleSystem.Animation;
using NewBattleSystem.Units.Core;

namespace EnemyUnit
{
    public class EnemyStatesHolder : IDisposable, IOnUpdate, IOnFixedUpdate
    {
        private readonly Dictionary<EnemyStateType, EnemyBaseState> _enemyStates 
            = new Dictionary<EnemyStateType, EnemyBaseState>();

        private event Action<EnemyStateType> _onStateCompeteCallBack;     
        private bool _isEnable = false;
          
        private EnemyBaseState _currentState;
        public EnemyBaseState CurrentState 
        {
            get => _currentState;
            private set
            {
                _currentState = value;
            }
        }
  
        public EnemyStatesHolder(
            EnemyModel model,
            UnitAnimationController animationController, 
            EnemyCore core)
        {
            _enemyStates[EnemyStateType.Idle] = new EnemyIdleState(model, animationController);
            _enemyStates[EnemyStateType.Move] = new EnemyMoveState(model, animationController, core);
            _enemyStates[EnemyStateType.Attack] = new EnemyAttackState(model, animationController);
            _enemyStates[EnemyStateType.SearchForTarget] = new EnemySearchForTargetState(model, animationController, core);
            _enemyStates[EnemyStateType.ChangeTarget] = new EnemyChangeTargetState(model, animationController);
            _enemyStates[EnemyStateType.Dying] = new EnemyDyingState(model, animationController);
        }

        public void Enable(Action<EnemyStateType> onStateCompeteCallBack)
        {
            _onStateCompeteCallBack = onStateCompeteCallBack;
            _isEnable = true;
        }

        public void Disable()
        {
            _onStateCompeteCallBack = null;
            _isEnable = false;
        }

        public void ChangeState(EnemyStateType targetState)
        {
            if (targetState == EnemyStateType.None)
                return;

            if(CurrentState != null)
            {
                CurrentState.OnStateComplete -= OnCurrentStateComplete;
                CurrentState.OnExit();
            }

            InitializeCurrentState(targetState);
        }

        private void InitializeCurrentState(EnemyStateType state) 
        {
            CurrentState = _enemyStates[state];
            CurrentState.OnEnter();
            CurrentState.OnStateComplete += OnCurrentStateComplete;
        }

        private void OnCurrentStateComplete(EnemyStateType stateType) 
        {      
            _onStateCompeteCallBack?.Invoke(stateType);
        }

        public void OnUpdate(float deltaTime)
        {
            if (!_isEnable)
                return;

            CurrentState.OnUpdate(deltaTime);
        }

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            if (!_isEnable)
                return;

            CurrentState.OnFixedUpdate(fixedDeltaTime);
        }

        public void Dispose()
        {
            Disable();

            CurrentState.OnStateComplete -= OnCurrentStateComplete;
            CurrentState.OnExit();

            CurrentState = default;
            
            _enemyStates.Clear();
        }
    }
}
