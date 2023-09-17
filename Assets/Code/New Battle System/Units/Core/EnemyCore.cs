using System;
using EnemyUnit;
using NewBattleSystem;
using UnityEngine;

namespace NewBattleSystem.Units.Core
{
    public class EnemyCore :IDisposable
    {
        private readonly EnemyView _view;
        private readonly IEnemyCoreData _data;

        private readonly EnemyTargetFinder _findTarget;
        private readonly PlanRouteMaker _planRoute;
        private readonly TargetPosotionHandler _target;

        public PlanRouteMaker PlanRoute => _planRoute;
        public EnemyTargetFinder FindTarget => _findTarget;
        public TargetPosotionHandler Target => _target;



        public EnemyCore(
            EnemyModel model, 
            EnemyView view, 
            IEnemyCoreData data,
            Transform primaryTarget)
        {
            _view = view;
            _data = data;

            _findTarget = new EnemyTargetFinder(_view, _data, primaryTarget);
            
            _planRoute = new PlanRouteMaker(_view, _data);

            _target = new TargetPosotionHandler(primaryTarget);
        }

        public void EnableCore(Vector3 position)
        {
            _planRoute.Initialize(position);
        }

        public void DisableCore()
        {
            _planRoute.Terminate();
        }

        public bool IsNearTo(Vector3 position)
        {
            var distance = GetDistanceTo(position);

            return distance < _data.StopDistance;
        }

        public float GetDistanceTo(Vector3 targetPos)
        {
            var unitPosition = _view.Position;
            var targetPosition = targetPos;
            
            return Vector3.Distance(targetPosition, unitPosition);
        }

        public void Dispose()
        {
            DisableCore();
        }
    }
}
