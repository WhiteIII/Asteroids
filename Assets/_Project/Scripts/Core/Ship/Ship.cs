using Codice.CM.Client.Differences;
using UnityEngine;
using Zenject;
using static UnityEngine.Time;

namespace _Project.Scripts.Core.Ship
{
    public class Ship : IInitializable
    {
        private readonly Movement _movement;
        private readonly RotationController _rotationController;
        private readonly GameObject _shipGameObject;
        
        public Vector3 Position => _movement.Position.Value;
        public Quaternion Rotation => _rotationController.ShipRotation.Value;

        public Ship(
            Movement movement,
            RotationController rotationController, 
            GameObject shipGameObject)
        {
            _movement = movement;
            _rotationController = rotationController;
            _shipGameObject = shipGameObject;
        }

        public void Initialize()
        {
            _movement.Initialize();
        }
        
        public void Enable() =>
            _shipGameObject.SetActive(true);
        
        public void Disable() =>
            _shipGameObject.SetActive(false);
        
        public void SetTransformPosition(Vector3 position) =>
            _movement.SetPosition(position);
        
        public void SetTransformRotation(Quaternion rotation) =>
            _rotationController.SetRotation(rotation);
    }
}
