using System;
using NewBattleSystem.Animation;

namespace EnemyUnit.EnemyStates
{
    public abstract class EnemyBaseState : IOnUpdate, IOnFixedUpdate
    {
        protected readonly EnemyModel _unit;
        protected readonly UnitAnimationController _animationController;

        public Action<EnemyStateType> OnStateComplete;

        public EnemyBaseState(
            EnemyModel unit,
            UnitAnimationController animationController) 
        {
            _unit = unit;
            _animationController = animationController;
        }

        public abstract void OnEnter();

        public abstract void OnExit();

        public abstract void OnUpdate(float deltaTime);
        public abstract void OnFixedUpdate(float fixedDeltaTime);
    }
}
