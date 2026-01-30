namespace _Project.Scripts.Data.Base
{
    public interface IAsteroidsData : IEnemyData
    {
        int SmallAsteroidsPoints { get; }
        float DirectionDeviationFrom { get; }
        float DirectionDeviationTo { get; }
        float RandomSpeedFrom { get; }
        float RandomSpeedTo { get; }
        int SpawnedSmallAsteroidsOnDeadCountForm { get; }
        int SpawnedSmallAsteroidsOnDeadCountTo { get; }
    }
}