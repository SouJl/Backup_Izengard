using System;
using System.Collections.Generic;
using EnemyUnit;
using EnemyUnit.Interfaces;
using NewBattleSystem;
using SpawnSystem.View;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpawnSystem
{
    public class EnemySpawnController : IOnUpdate, IOnFixedUpdate, IDisposable
    {
        private readonly Dictionary<int, IEnemyController> _enemies 
            = new Dictionary<int, IEnemyController>();

        private readonly EnemySpawnView _spawnView;
        private readonly EnemySpawnSettings _settings;
        private readonly EnemyFactory _enemyFactory;
        private readonly EnemyPool _enemyPool;

        public EnemySpawnController(
            EnemySpawnView spawnView,
            EnemySpawnSettings settings,
            Transform primaryTarget)
        {
            _spawnView = spawnView;
            _settings = settings;
            _enemyFactory = new EnemyFactory(_spawnView.PoolHolder, primaryTarget);
            _enemyPool = new EnemyPool(_enemyFactory, _settings.EnemiesDataList);
        }

        public void SpawnEnemy(EnemyType enemyType)
        {
            var enemy = _enemyPool.GetFromPool(enemyType);
            
            var spwanPoint = _spawnView.SpawnPoints[Random.Range(0, _spawnView.SpawnPoints.Count)];

            enemy.Init(spwanPoint.position);
          
            enemy.OnDeath += OnEnemyDestroy;            
            _enemies[enemy.Index] = enemy;
        }


        public void OnEnemyDestroy(int enemyId) 
        {
            var enemy = _enemies[enemyId];
            
            enemy.Disable();
            
            enemy.OnDeath -= OnEnemyDestroy;
            
            _enemyPool.ReturnToPool(enemy);
            _enemies.Remove(enemyId);
        }

        public void OnUpdate(float deltaTime)
        {
            foreach(var enemy in _enemies)
            {
                enemy.Value.OnUpdate(deltaTime);
            }
        }

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            foreach (var enemy in _enemies)
            {
                enemy.Value.OnFixedUpdate(fixedDeltaTime);
            }
        }

        public void Dispose()
        {
            foreach (var enemy in _enemies)
            {
                enemy.Value?.Dispose();
            }

            _enemies.Clear();

            _enemyPool?.Dispose();
        }
    }
}
