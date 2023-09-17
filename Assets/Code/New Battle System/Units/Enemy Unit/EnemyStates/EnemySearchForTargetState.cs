using NewBattleSystem.Animation;
using NewBattleSystem.Units.Core;

namespace EnemyUnit.EnemyStates
{
    public class EnemySearchForTargetState : EnemyBaseState
    {
        private readonly EnemyCore _core;

        private bool _isOnSearh;

        public EnemySearchForTargetState(
            EnemyModel unit,
            UnitAnimationController animationController,
            EnemyCore core) : base(unit, animationController)
        {
            _core = core;
        }

        public override void OnEnter()
        {
            _core.FindTarget.OnComplete += OnSearchComplete;
            _core.FindTarget.SearchForTarget();
            _isOnSearh = true;
        }

        public override void OnExit()
        {
            _core.FindTarget.OnComplete -= OnSearchComplete;
        }

        public override void OnUpdate(float deltaTime)
        {
            if (!_isOnSearh)
                return;

            _core.FindTarget.OnUpdate(deltaTime);
        }

        public override void OnFixedUpdate(float fixedDeltaTime) { }

        private void OnSearchComplete()
        {
            _isOnSearh = false;
            OnStateComplete?.Invoke(EnemyStateType.SearchForTarget);
        }
    }
}
