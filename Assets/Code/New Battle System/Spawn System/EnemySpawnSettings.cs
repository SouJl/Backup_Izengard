using EnemyUnit;
using System.Collections.Generic;
using UnityEngine;

namespace SpawnSystem
{
    [CreateAssetMenu(fileName = nameof(EnemySpawnSettings), menuName = "NewBattleSystem/Enemy/" + nameof(EnemySpawnSettings))]
    public class EnemySpawnSettings : ScriptableObject
    {
        [field: SerializeField] public float SpawnRate { get; private set; } = 1.0f;

        [field: SerializeField] public List<EnemySettings> EnemiesDataList;

    }
}
