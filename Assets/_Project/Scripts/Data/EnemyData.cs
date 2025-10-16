using UnityEngine;

namespace _Project.Scripts.Data
{
    public class EnemyData : ScriptableObject
    {
        [field: SerializeField] public int Points { get; private set; }
    }
}