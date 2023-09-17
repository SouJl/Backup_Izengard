using NewBattleSystem.Animation;

namespace EnemyUnit.EnemyStates
{
    public class EnemyDyingState : EnemyBaseState
    {
        public EnemyDyingState(
            EnemyModel unit,
            UnitAnimationController animationController) : base(unit, animationController)
        {

        }

        public override void OnEnter()
        {
            _animationController.PlayAnimation(UnitAnimationType.Dying);
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
