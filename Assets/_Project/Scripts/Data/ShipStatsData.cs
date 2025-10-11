using UnityEngine;

namespace _Project.Scripts.Data
{
    [CreateAssetMenu(menuName = "_Project/ShipStats", fileName = "ShipStats")]
    public class ShipStatsData : ScriptableObject
    {
        [field: SerializeField] public float MovementSpeed { get; private set; }
        [field: SerializeField] public float BulletFlyingSpeed { get; private set; }
        [field: SerializeField] public float RotationSpeed { get; private set; }
        [field: SerializeField] public float LazerActivityTime { get; private set; }
        [field: SerializeField] public float LazerRechargeTime { get; private set; }
        [field: SerializeField] public int LazerChargeCount { get; private set; }  
    }
}
