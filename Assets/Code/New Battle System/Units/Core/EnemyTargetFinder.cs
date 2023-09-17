using System;
using EnemyUnit;
using TMPro;
using UnityEngine;

namespace NewBattleSystem.Units.Core
{
    public class EnemyTargetFinder : IOnUpdate
    {
        private const int BUILDING_LAYER_NUMBER = 10;
        private const int WALL_LAYER_NUMBER = 15;
        private const int SEARCH_FRAMES = 10;
  
        private readonly Vector3 _rayCastDirection = Vector3.forward;

        private readonly EnemyView _view;
        private readonly IEnemyCoreData _data;
        private readonly Transform _primaryTarget;

        private Vector3 _targetFindPosition;

        private bool _searchStart;
        private int _frameCount;

        public event Action OnComplete;

        public Vector3 TargetFindPosition => _targetFindPosition;

        public EnemyTargetFinder(
            EnemyView view,
            IEnemyCoreData data,
            Transform primaryTarget)
        {
            _view = view;
            _data = data;
            _primaryTarget = primaryTarget;

            _searchStart = false;
        }

        public void SearchForTarget() 
        { 
            _searchStart = true;
            _frameCount = SEARCH_FRAMES;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_searchStart == false)
                return;

            if (_frameCount <= 0)
            {
                _targetFindPosition = FindTarget();
                OnComplete?.Invoke();

                _frameCount = SEARCH_FRAMES;
            }
            else
            {
                _frameCount--;
            }
        }

        private Vector3 FindTarget()
        {
            var direction = _primaryTarget.position - _view.transform.position;

            var ray = new Ray(_view.transform.position, direction.normalized);

            //var testHitts = Physics.RaycastAll(ray, Mathf.Infinity, _data.SearhMask);

            if (Physics.Raycast(ray, out var hitData, Mathf.Infinity, _data.SearhMask))
            {
                var findLayer = hitData.transform.gameObject.layer;
                if (findLayer == BUILDING_LAYER_NUMBER)
                {
                    return hitData.transform.position;
                }
                else if (findLayer == WALL_LAYER_NUMBER)
                {
                    var hitPoint = hitData.transform.position;

                    var randomPointInRadius
                        = hitPoint
                        + UnityEngine.Random.insideUnitSphere * _data.OnHitSearchDistance;

                    return new Vector3(hitPoint.x, hitPoint.y, randomPointInRadius.z);
                }
              
            }
            return _primaryTarget.position;
        }
    }
}