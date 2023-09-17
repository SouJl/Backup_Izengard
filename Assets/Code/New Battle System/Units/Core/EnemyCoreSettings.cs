using UnityEngine;

namespace NewBattleSystem.Units.Core
{
    [CreateAssetMenu(fileName = nameof(EnemyCoreSettings), menuName = "NewBattleSystem/Enemy/" + nameof(EnemyCoreSettings))]
    public class EnemyCoreSettings : ScriptableObject, IEnemyCoreData
    {
        [field: SerializeField] public float AttackMaxDistance { get; private set; } = 1.0f;
        [field: SerializeField] public float AttackSpeed { get; private set; } = 1.0f;

        [field: SerializeField] public float OnHitSearchDistance { get; private set; } = 2.5f;

        [field: SerializeField] public float StopDistance { get; private set; } = 0.5f;

        [field: SerializeField] public float MoveSpeed { get; private set; } = 1.0f;

        [SerializeField] private LayerMask _searchMask;

        public int SearhMask => _searchMask.value;
    }
}
