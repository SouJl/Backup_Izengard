using EnemyUnit;
using NewBattleSystem.Navigation;
using SpawnSystem;
using SpawnSystem.View;
using UnityEngine;

namespace NewBattleSystem
{
    public class BattleTestEntryPoint : MonoBehaviour
    {
        [SerializeField] private NavigationSurfaceView _ground;
        [SerializeField] private BattleSystemConfig battleSystemConfig;
        [SerializeField] private EnemySpawnView _enemySpawn;
        [SerializeField] private BuildingView _mainTower;

        private NavigationUpdater _navigationUpdater;
        private EnemySpawnController _enemySpawnController;

        private int _groundSurfaceIndex;

        private void Start()
        {
            _navigationUpdater = new NavigationUpdater();
            _groundSurfaceIndex = _navigationUpdater.AddNavigationSurface(_ground);

            _enemySpawnController
                = new EnemySpawnController(_enemySpawn, battleSystemConfig.EnemySpawnSettings, _mainTower.SelfTransform);

            _enemySpawnController.SpawnEnemy(EnemyType.Hound);
            _enemySpawnController.SpawnEnemy(EnemyType.Archer);
            _enemySpawnController.SpawnEnemy(EnemyType.Archer);
        }

        private void Update()
        {
            _enemySpawnController.OnUpdate(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            _enemySpawnController.OnFixedUpdate(Time.fixedDeltaTime);
        }
    }
}
