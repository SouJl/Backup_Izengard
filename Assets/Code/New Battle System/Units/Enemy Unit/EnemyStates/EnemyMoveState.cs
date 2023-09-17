using NewBattleSystem.Animation;
using NewBattleSystem.Units.Core;

namespace EnemyUnit.EnemyStates
{
    public class EnemyMoveState : EnemyBaseState
    {
        private readonly EnemyCore _core;

        public EnemyMoveState(
            EnemyModel unit,
            UnitAnimationController animationController,
            EnemyCore core) : base(unit, animationController)
        {
            _core = core;
        }

        public override void OnEnter()
        {
            _animationController.PlayAnimation(UnitAnimationType.Move);

            var targetPos = _core.Target.Position;
            _core.PlanRoute.SetTargetRoute(targetPos);
        }

        public override void OnExit()
        {
            _animationController.StopAnimation();
            _core.PlanRoute.ResetRoute();
        }

        public override void OnUpdate(float deltaTime)
        {
        }

        public override void OnFixedUpdate(float fixedDeltaTime)
        {
            var targetPos = _core.Target.Position;
            
            if (_core.IsNearTo(targetPos))
            {
                OnStateComplete?.Invoke(EnemyStateType.Move);
            }
        }
    }
}
