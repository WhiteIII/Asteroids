namespace _Project.Scripts.Core.Stats
{
    public class ShipStats : IShipStats
    {
        public float MovementSpeed { get; private set; }
        
        public float SetMovementSpeed(float movementSpeed) => 
            MovementSpeed = movementSpeed;
    }

    public interface IShipStats : IMovementSpeedStats
    {
        
    }
}
