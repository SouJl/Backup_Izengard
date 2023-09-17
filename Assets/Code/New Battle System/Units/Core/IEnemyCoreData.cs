using UnityEngine;

namespace NewBattleSystem.Units.Core
{
    public interface IEnemyCoreData
    {
        public float AttackMaxDistance { get; }
        public float AttackSpeed { get; }
        public float OnHitSearchDistance { get; }   

        public float StopDistance { get; }

        public float MoveSpeed { get; }

        public int SearhMask { get; }
    }
}
