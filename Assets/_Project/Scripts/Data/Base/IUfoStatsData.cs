namespace _Project.Scripts.Data.Base
{
    public interface IUfoStatsData : IEnemyData
    {
        float MovementSpeed { get; }
        float BulletFlyingSpeed { get; }
        float AttackCooldown { get; }
        float AttackDistance { get; }
    }
}