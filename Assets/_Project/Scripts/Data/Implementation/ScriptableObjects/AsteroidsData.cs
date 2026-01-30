using _Project.Scripts.Data.Base;
using UnityEngine;

namespace _Project.Scripts.Data.Implementation
{
    [CreateAssetMenu(menuName = "_Project/AsteroidsData", fileName = "AsteroidsData")]
    public class AsteroidsData : EnemyData, IAsteroidsData
    {
        [field: SerializeField] public int SmallAsteroidsPoints { get; private set; }
        [field: SerializeField] public float DirectionDeviationFrom { get; private set; }
        [field: SerializeField] public float DirectionDeviationTo { get; private set; }
        [field: SerializeField] public float RandomSpeedFrom { get; private set; }
        [field: SerializeField] public float RandomSpeedTo { get; private set; }
        [field: SerializeField] public int SpawnedSmallAsteroidsOnDeadCountForm { get; private set; } = 1;
        [field: SerializeField] public int SpawnedSmallAsteroidsOnDeadCountTo { get; private set; } = 3;
    }
}