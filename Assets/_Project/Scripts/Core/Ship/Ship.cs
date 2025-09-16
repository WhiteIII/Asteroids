using Codice.CM.Client.Differences;
using UnityEngine;
using Zenject;
using static UnityEngine.Time;

namespace _Project.Scripts.Core.Ship
{
    public class Ship
    {
        private readonly Movement _movement;
        
        public Vector3 Position => _movement.Position.Value;

        public Ship(Movement movement)
        {
            _movement = movement;
        }
    }
}
