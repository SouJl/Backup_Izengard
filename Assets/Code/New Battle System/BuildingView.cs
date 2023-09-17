using System;
using UnityEngine;

namespace NewBattleSystem
{
    public class BuildingView : MonoBehaviour, IDamageable
    {      

        [SerializeField] private ScriptableObject _buildingConfig;
        
        private Transform _selfTransform;

        public ScriptableObject BuildingConfig => _buildingConfig;
        public Transform SelfTransform => _selfTransform;

        private void Awake()
        {
            _selfTransform = transform;
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
