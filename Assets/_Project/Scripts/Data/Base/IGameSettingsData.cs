namespace _Project.Scripts.Data.Base
{
    public interface IGameSettingsData : IData
    {
        float SpawnOffsetOutSideCameraVision { get; }
        float AsteroidsSpawnCoolDown { get; }
        float UfoSpawnCoolDown { get; }
    }
}