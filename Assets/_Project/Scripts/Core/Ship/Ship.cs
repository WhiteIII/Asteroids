using Codice.CM.Client.Differences;
using UnityEngine;
using Zenject;
using static UnityEngine.Time;

namespace _Project.Scripts.Core.Ship
{
    public class Ship : IInitializable
    {
        private readonly Movement _movement;
        
        public Vector3 Position => _movement.Position.Value;

        public Ship(Movement movement)
        {
            _movement = movement;
        }

        public void Initialize()
        {
            _movement.Initialize();
        }
        
        public void SetTransformPosition(Vector3 position) =>
            _movement.SetPosition(position);
    }
}
