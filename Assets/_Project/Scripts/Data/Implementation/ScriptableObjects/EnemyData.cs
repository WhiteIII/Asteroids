using _Project.Scripts.Data.Base;
using UnityEngine;

namespace _Project.Scripts.Data.Implementation
{
    public class EnemyData : ScriptableObject, IEnemyData
    {
        [field: SerializeField] public int Points { get; private set; }
    }
}