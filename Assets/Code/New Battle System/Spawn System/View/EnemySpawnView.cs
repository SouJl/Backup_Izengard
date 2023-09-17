using System.Collections.Generic;
using UnityEngine;

namespace SpawnSystem.View
{
    public class EnemySpawnView :  MonoBehaviour
    {
        [SerializeField] private Transform _poolHolder;
        [SerializeField] private List<Transform> _spawnPoints;

        public Transform PoolHolder => _poolHolder;

        public List<Transform> SpawnPoints => _spawnPoints;
    }
}
