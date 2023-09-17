using System;
using CombatSystem;
using EnemyUnit;
using NewBattleSystem;

namespace NewBattleSystem.Units.Core
{
    public class RangedAttackAction :  IOnUpdate
    {
        private readonly EnemyModel _model;
        private readonly EnemyView _view;
        private readonly IEnemyCoreData _data;
        private readonly IBulletsController _bulletsController;
        private float _actionTime;
        private float _timeCounter;
        private IDamageable _target;
        private bool _isBulletLaunched;
        private bool _haveTarget;
        
        public event Action<IDamageable> OnComplete;

        public RangedAttackAction(
            EnemyModel model,
            EnemyView view,
            IEnemyCoreData data,
            IBulletsController bulletsController)
        {
            _bulletsController = bulletsController;
            _model = model;
            _view = view;
            _data = data;
            _haveTarget = false;
        }

        public void StartAction(IDamageable target)
        {
            _actionTime = 1 / _data.AttackSpeed;
            _timeCounter = _actionTime;
            _isBulletLaunched = false;
            _target = target;
            _haveTarget = true;
        }
        
        public void ClearTarget()
        {
            _target = null;
            _haveTarget = false;
        }

        public void OnUpdate(float deltaTime)
        {
            if (!_haveTarget)
            {
                OnComplete?.Invoke(null);
            }
            else if (_timeCounter > 0)
            {
                _timeCounter -= deltaTime;
                if (_timeCounter < _actionTime / 2 && !_isBulletLaunched) LaunchBullet();
                if (_timeCounter <= 0) OnComplete?.Invoke(_target);
            }
        }

        private void LaunchBullet()
        {
            _isBulletLaunched = true;
            var bullet = _bulletsController.BulletsPool.GetObjectFromPool();
            bullet.StartFlight(_target.Position, _view.transform.position);
            _bulletsController.AddBullet(bullet);
            bullet.BulletFlightIsOver += BulletFlightOver;
        }

        private void BulletFlightOver(IBulletController bullet)
        {
            _bulletsController.RemoveBullet(bullet);
            bullet.BulletFlightIsOver -= BulletFlightOver;
            if (_target != null)
            {
                _target.TakeDamage(_model.AttackDamage, _view);
            }
        }
    }
}