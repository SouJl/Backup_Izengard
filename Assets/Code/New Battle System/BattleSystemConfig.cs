using SpawnSystem;
using UnityEngine;

namespace NewBattleSystem
{
    [CreateAssetMenu(fileName = nameof(BattleSystemConfig), menuName = "NewBattleSystem/" + nameof(BattleSystemConfig))]
    public class BattleSystemConfig : ScriptableObject
    {
        [field: SerializeField] public EnemySpawnSettings EnemySpawnSettings;
    }
}
