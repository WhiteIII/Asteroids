using UnityEngine;

namespace _Project.Scripts.Data
{
    [CreateAssetMenu(menuName = "_Project/GameSettings", fileName = "GameSettings")]
    public class GameSettingsData : ScriptableObject
    {
        [field: SerializeField] public float SpawnOffsetOutSideCameraVision { get; private set; }
    }
}