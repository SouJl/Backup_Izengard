using NewBattleSystem.Units.Core;
using UnityEngine;

namespace EnemyUnit
{
    [CreateAssetMenu(fileName = nameof(EnemySettings), menuName = "NewBattleSystem/Enemy/" + nameof(EnemySettings))]
    public class EnemySettings : ScriptableObject
    {
        [field: SerializeField] public EnemyType Type { get; private set; }
        [field: SerializeField] public int MaxHelth { get; private set; } = 100;
        [field: SerializeField] public int AttackDamage { get; private set; } = 10;
        [field: SerializeField] public int DefeatCost { get; private set; } = 10;
        [field: SerializeField] public int MaxUnitsInPool { get; private set; } = 5;
        [field: SerializeField] public EnemyCoreSettings Core { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }
    }
}
