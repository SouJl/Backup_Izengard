using EnemyUnit;
using UnityEngine;
using UnityEngine.AI;

namespace NewBattleSystem.Units.Core
{
    public class PlanRouteMaker
    {
        private readonly NavMeshAgent _navMeshAgent;
        private readonly IEnemyCoreData _data;

      //  public event Action<IDamageable> OnComplete;

        public PlanRouteMaker(EnemyView view, IEnemyCoreData data)
        {
            _navMeshAgent = view.NavMesh;
            _data = data;
        }

        public void Initialize(Vector3 initPos)
        {
            _navMeshAgent.enabled = true;

            if (_navMeshAgent.isOnNavMesh)
                _navMeshAgent.ResetPath();

            _navMeshAgent.speed = _data.MoveSpeed;

            _navMeshAgent.Warp(initPos);
        }

        public void Terminate()
        {
            if (_navMeshAgent.isOnNavMesh)
                _navMeshAgent.ResetPath();

            _navMeshAgent.enabled = false;
        }

        public void SetTargetRoute(Vector3 targetPos)
        {
            if (_navMeshAgent.gameObject.activeSelf && _navMeshAgent.isOnNavMesh)
                _navMeshAgent.SetDestination(targetPos);
        }

        public void ResetRoute()
        {
            if (_navMeshAgent.gameObject.activeInHierarchy && _navMeshAgent.isOnNavMesh)
            {
                _navMeshAgent.ResetPath();
            }
        }
    }
}
