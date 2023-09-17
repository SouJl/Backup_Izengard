using System;
using NewBattleSystem;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyUnit
{
    public class EnemyView : MonoBehaviour, IDamageable
    {
        [SerializeField] private NavMeshAgent _navMesh;
        [SerializeField] private Animator _animator;

        public NavMeshAgent NavMesh => _navMesh;
        public Animator Animator => _animator;

        public void SetActive(bool state) 
        {
            gameObject.SetActive(state);
        }

        public void ChangePosition(Vector3 position)
        {
            transform.position = position;
        }

        #region IDamageable

        public Vector3 Position => transform.position;

        public event Action<int> OnTakeDamage;

        public void TakeDamage(int damage, IDamageable damageDealer)
        {
            OnTakeDamage?.Invoke(damage);
        }

        #endregion
    }
}
