using EnemyUnit.Interfaces;
using NewBattleSystem.Animation;
using NewBattleSystem.Units.Core;
using UnityEngine;

namespace EnemyUnit
{
    public class EnemyFactory 
    {
        private readonly Transform _instantiatePos;
        private readonly Transform _target;

        private int _enemyIndex = 0;

        public EnemyFactory(
            Transform instantiatePos,
            Transform target)
        {
            _instantiatePos = instantiatePos;
            _target = target;
        }

        public IEnemyController CreateEnemy(EnemySettings enemyData)
        {
            var enemy = enemyData.Type switch
            {
                EnemyType.Tank => CreateTank(enemyData),
                EnemyType.Archer => CreateArcher(enemyData),
                EnemyType.Hound => CreateHound(enemyData),
                EnemyType.Boss => CreateBoss(enemyData),
                _ => new StubEnemyController(),
            };

            enemy.SetIndex(_enemyIndex++);
            return enemy;
        }

        public IEnemyController CreateTank(EnemySettings enemyData)
        {
            var enemyObj = Object.Instantiate(enemyData.Prefab, _instantiatePos);

            var view = enemyObj.GetComponent<EnemyView>();
            var model = new EnemyModel(enemyData);
            var core = new EnemyCore(model, view, enemyData.Core, _target);

            var animation = new UnitAnimationController();

            var statesholder = new EnemyStatesHolder(model, animation, core);

            return new EnemyController(model, view, core, statesholder);
        }

        public IEnemyController CreateArcher(EnemySettings enemyData)
        {
            var enemyObj = Object.Instantiate(enemyData.Prefab, _instantiatePos);

            var view = enemyObj.GetComponent<EnemyView>();
            var model = new EnemyModel(enemyData);
            //Доработать отдельно для Archer Enemy
            var core = new EnemyCore(model, view, enemyData.Core, _target);

            var animation = new UnitAnimationController();

            var statesholder = new EnemyStatesHolder(model, animation, core);

            return new EnemyController(model, view, core, statesholder);
        }

        public IEnemyController CreateHound(EnemySettings enemyData)
        {
            var enemyObj = Object.Instantiate(enemyData.Prefab, _instantiatePos);

            var view = enemyObj.GetComponent<EnemyView>();
            var model = new EnemyModel(enemyData);
            //Доработать отдельно для Hound Enemy
            var core = new EnemyCore(model, view, enemyData.Core, _target);

            var animation = new UnitAnimationController();

            var statesholder = new EnemyStatesHolder(model, animation, core);

            return new EnemyController(model, view, core, statesholder);
        }

        public IEnemyController CreateBoss(EnemySettings enemyData)
        {
            var enemyObj = Object.Instantiate(enemyData.Prefab, _instantiatePos);

            var view = enemyObj.GetComponent<EnemyView>();
            var model = new EnemyModel(enemyData);
            //Доработать отдельно для Boss Enemy
            var core = new EnemyCore(model, view, enemyData.Core, _target);

            var animation = new UnitAnimationController();

            var statesholder = new EnemyStatesHolder(model, animation, core);

            return new EnemyController(model, view, core, statesholder);
        }
    }
}
