using System;
using _Project.Scripts.Data.Base;

namespace _Project.Scripts.Data.Implementation
{
    [Serializable]
    public class UfoStatsConfig : IUfoStatsData
    {
        public float MovementSpeed { get; set; }
        public float BulletFlyingSpeed { get; set; }
        public float AttackCooldown { get; set; }
        public float AttackDistance { get; set; }
        public int Points { get; set; }

        public UfoStatsConfig SetData(IUfoStatsData shipStatsDataB)
        {
            MovementSpeed = shipStatsDataB.MovementSpeed;
            BulletFlyingSpeed = shipStatsDataB.BulletFlyingSpeed;
            AttackCooldown = shipStatsDataB.AttackCooldown;
            AttackDistance = shipStatsDataB.AttackDistance;
            Points = shipStatsDataB.Points;
            return this;
        }
    }
}