using Codice.CM.Client.Differences;
using UnityEngine;
using Zenject;
using static UnityEngine.Time;

namespace _Project.Scripts.Core.Ship
{
    internal class Ship
    {
        private readonly Movement _movement;
        
        public Vector3 Position => _movement.Position.Value;
    }
}
