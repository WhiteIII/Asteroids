namespace _Project.Scripts.Data.Base
{
    public interface IShipStatsData : IData
    {
        float MovementSpeed { get; }
        float BulletFlyingSpeed { get; }
        float RotationSpeed { get; }
        float LazerActivityTime { get; }
        float LazerRechargeTime { get; }
        int LazerChargeCount { get; }
    }
}