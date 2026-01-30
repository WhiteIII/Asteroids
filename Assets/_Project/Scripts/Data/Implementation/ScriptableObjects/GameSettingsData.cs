using _Project.Scripts.Data.Base;
using UnityEngine;

namespace _Project.Scripts.Data.Implementation
{
    [CreateAssetMenu(menuName = "_Project/GameSettings", fileName = "GameSettings")]
    public class GameSettingsData : ScriptableObject, IGameSettingsData
    {
        [field: SerializeField] public float SpawnOffsetOutSideCameraVision { get; private set; }
        [field: SerializeField] public float AsteroidsSpawnCoolDown { get; private set; }
        [field: SerializeField] public float UfoSpawnCoolDown { get; private set; }
    }
}