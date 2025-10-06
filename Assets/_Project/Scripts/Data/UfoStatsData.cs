using UnityEngine;

namespace _Project.Scripts.Data
{
    [CreateAssetMenu(menuName = "_Project/UfoStatsData", fileName = "UfoStatsData")]
    public class UfoStatsData : ScriptableObject
    {
        [field: SerializeField] public float MovementSpeed { get; private set; }
        [field: SerializeField] public float BulletFlyingSpeed { get; private set; }
        [field: SerializeField] public float AttackCooldown { get; private set; }
        [field: SerializeField] public float AttackDistance { get; private set; }
    }
}