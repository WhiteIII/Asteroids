using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core.Services
{
    internal class ShipFactory : IFactory<Ship.Ship>
    {
        private readonly IInstantiator _instantiator;
        private readonly GameObject _shipPrefab; 
        
        public Ship.Ship Create()
        {
            throw new System.NotImplementedException();
        }
    }
}
