using System;
using _Project.Scripts.Data.Base;

namespace _Project.Scripts.Data.Implementation
{
    [Serializable]
    public class GameSettingsConfig : IGameSettingsData
    {
        public float SpawnOffsetOutSideCameraVision { get; set; }
        public float AsteroidsSpawnCoolDown { get; set; }
        public float UfoSpawnCoolDown { get; set; }

        public GameSettingsConfig SetData(IGameSettingsData data)
        {
            SpawnOffsetOutSideCameraVision = data.SpawnOffsetOutSideCameraVision;
            AsteroidsSpawnCoolDown = data.AsteroidsSpawnCoolDown;
            UfoSpawnCoolDown = data.UfoSpawnCoolDown;
            return this;
        }
    }
}