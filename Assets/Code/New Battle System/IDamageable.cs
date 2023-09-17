using System;
using UnityEngine;

namespace NewBattleSystem
{
    public interface IDamageable
    {
        Vector3 Position { get; }

        event Action<int> OnTakeDamage;

        void TakeDamage(int damage, IDamageable damageDealer);
    }
}
