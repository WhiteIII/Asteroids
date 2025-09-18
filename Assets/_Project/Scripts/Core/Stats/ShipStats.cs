namespace _Project.Scripts.Core.Stats
{
    public class ShipStats : IShipStats
    {
        public float MovementSpeed { get; private set; }
        public float RotationSpeed { get; private set; }
        public float BulletSpeed { get; private set; }
        
        public void SetMovementSpeed(float movementSpeed) => 
            MovementSpeed = movementSpeed;
        
        public void SetRotationSpeed(float rotationSpeed) => 
            RotationSpeed = rotationSpeed;

        public void SetBulletSpeed(float bulletSpeed) =>
            BulletSpeed = bulletSpeed;
    }

    public interface IShipStats : 
        IMovementSpeedStats,
        IRotationSpeedStats,
        IBulletSpeedStats
    {
        
    }
}
