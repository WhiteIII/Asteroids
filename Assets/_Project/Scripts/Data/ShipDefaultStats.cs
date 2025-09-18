using UnityEngine;

namespace _Project.Scripts.Data
{
    [CreateAssetMenu(fileName = "ShipDefaultStats", menuName = "Project/ShipDefaultStats")]
    public class ShipDefaultStats : ScriptableObject, IShipDefaultStats
    {
        [field: SerializeField] public float MovementSpeed { get; private set; }
        [field: SerializeField] public float RotationSpeed { get; private set; }
    }
}