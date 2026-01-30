using System;
using _Project.Scripts.Data.Base;

namespace _Project.Scripts.Data.Implementation
{
    [Serializable]
    public class ShipStatsConfig : IShipStatsData
    {
        public float MovementSpeed { get; set; }
        public float BulletFlyingSpeed { get; set; }
        public float RotationSpeed { get; set; }
        public float LazerActivityTime { get; set; }
        public float LazerRechargeTime { get; set; }
        public int LazerChargeCount { get; set; }

        public ShipStatsConfig SetData(IShipStatsData shipStatsDataB)
        {
            MovementSpeed = shipStatsDataB.MovementSpeed;
            BulletFlyingSpeed = shipStatsDataB.BulletFlyingSpeed;
            RotationSpeed = shipStatsDataB.RotationSpeed;
            LazerActivityTime = shipStatsDataB.LazerActivityTime;
            LazerRechargeTime = shipStatsDataB.LazerRechargeTime;
            LazerChargeCount = shipStatsDataB.LazerChargeCount;
            return this;
        }
    }
}