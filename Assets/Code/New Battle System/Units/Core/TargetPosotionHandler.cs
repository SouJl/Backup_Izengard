using UnityEngine;

namespace NewBattleSystem.Units.Core
{
    public class TargetPosotionHandler
    {
        private Vector3 _position = Vector3.zero;

        public Vector3 Position => _position;

        public TargetPosotionHandler() { }

        public TargetPosotionHandler(Transform primaryTarget) 
        {
            _position = primaryTarget.position;
        }

        public void ChangePosition(Vector3 newPos)
        {
            _position = newPos;
        }
    }
}
