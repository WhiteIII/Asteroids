using UnityEngine;

namespace _Project.Scripts.Data
{
    public class ShipStats : ScriptableObject
    {
        [field: SerializeField] public float MovementSpeed { get; private set; }
        [field: SerializeField] public float BulletFlyingSpeed { get; private set; }
        [field: SerializeField] public float RotationSpeed { get; private set; }
    }
}
