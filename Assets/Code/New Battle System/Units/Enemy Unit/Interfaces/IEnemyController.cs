using System;
using UnityEngine;

namespace EnemyUnit.Interfaces
{
    public interface IEnemyController : IOnController, IOnUpdate, IOnFixedUpdate, IDisposable
    {
        int Index { get; }

        event Action<int> OnDeath;

        void Init(Vector3 startPosition);

        void SetIndex(int index);

        EnemyType GetEnemyType();

        void Disable();
    }
}
