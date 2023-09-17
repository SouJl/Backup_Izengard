﻿using NewBattleSystem.Animation;

namespace EnemyUnit.EnemyStates
{
    public class EnemyIdleState : EnemyBaseState
    {
        public EnemyIdleState(
            EnemyModel unit,
            UnitAnimationController animationController) : base(unit, animationController)
        {

        }

        public override void OnEnter()
        {
            _animationController.PlayAnimation(UnitAnimationType.Idle);
        }

        public override void OnExit()
        {
            _animationController.StopAnimation();
        }

        public override void OnUpdate(float deltaTime)
        {

        }

        public override void OnFixedUpdate(float fixedDeltaTime)
        {

        }
    }
}