using System;
using System.Collections.Generic;
using EnemyUnit.Interfaces;
using UnityEngine;

namespace EnemyUnit
{
    public class EnemyPool : IDisposable
    {
        private readonly Dictionary<EnemyType, Queue<IEnemyController>> _pool;

        private readonly EnemySettings _data;
        private readonly EnemyFactory _enemyFactory;

        public EnemyPool(EnemyFactory enemyFactory, List<EnemySettings> enemyDataList)
        {
            _pool = new Dictionary<EnemyType, Queue<IEnemyController>>();

            _enemyFactory = enemyFactory;

            FillPool(enemyDataList);
        }

        private void FillPool(List<EnemySettings> enemyDataList)
        {
            foreach (var enemydata in enemyDataList)
            {
                _pool[enemydata.Type] = new Queue<IEnemyController>();

                for (int i = 0; i < enemydata.MaxUnitsInPool; i++)
                {
                    var enemy = _enemyFactory.CreateEnemy(enemydata);

                    enemy.Disable();

                    _pool[enemydata.Type].Enqueue(enemy);
                }
            }
        }

        public IEnemyController GetFromPool(EnemyType type)
        {
            if (_pool[type].Count > 0)
            {
                var enemy = _pool[type].Dequeue();
                return enemy;
            }
            else
            {
                var enemy = _enemyFactory.CreateEnemy(_data);
                return enemy;
            }
        }

        public void ReturnToPool(IEnemyController enemy)
        {
            var type = enemy.GetEnemyType();
            _pool[type].Enqueue(enemy);
        }

        public void Dispose()
        {
            _pool.Clear();
        }
    }
}
