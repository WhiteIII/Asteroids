using Codice.CM.Client.Differences;
using UnityEngine;
using Zenject;
using static UnityEngine.Time;

namespace _Project.Scripts.Core.Ship
{
    public class Ship : IInitializable
    {
        private readonly ShipMovement _shipMovement;
        private readonly RotationController _rotationController;
        private readonly GameObject _shipGameObject;
        
        public Vector3 Position => _shipMovement.Position.Value;
        public Quaternion Rotation => _rotationController.ShipRotation.Value;

        public Ship(
            ShipMovement shipMovement,
            RotationController rotationController, 
            GameObject shipGameObject)
        {
            _shipMovement = shipMovement;
            _rotationController = rotationController;
            _shipGameObject = shipGameObject;
        }

        public void Initialize()
        {
            _shipMovement.Initialize();
        }
        
        public void Enable() =>
            _shipGameObject.SetActive(true);
        
        public void Disable() =>
            _shipGameObject.SetActive(false);
        
        public void SetTransformPosition(Vector3 position) =>
            _shipMovement.SetPosition(position);
        
        public void SetTransformRotation(Quaternion rotation) =>
            _rotationController.SetRotation(rotation);
    }
}
