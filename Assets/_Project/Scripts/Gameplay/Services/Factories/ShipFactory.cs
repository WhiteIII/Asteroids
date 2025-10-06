using _Project.Scripts.Data;
using _Project.Scripts.Gameplay.GameLoopSystem;
using _Project.Scripts.Gameplay.Services.Repositories;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Services.Factories
{
    public class ShipFactory : PlaceholderFactory<Ship.Ship>
    {
        private readonly GameObject _shipPrefab;
        private readonly IGameLoopCreator _creator;
        private readonly ShipStatsData _shipStatsData;
        private readonly CharactersRepository _repository;

        public ShipFactory(
            GameObject shipPrefab,
            IGameLoopCreator creator,
            ShipStatsData shipStatsData,
            CharactersRepository repository)
        {
            _shipPrefab = shipPrefab;
            _creator = creator;
            _shipStatsData = shipStatsData;
            _repository = repository;
        }

        public override Ship.Ship Create()
        {
            Ship.Ship ship = _repository.RegisterShip(_creator
                .CreateMonoBehaviourObject<Ship.Ship>(_shipPrefab));
            ship.Initialize(
                _shipStatsData.MovementSpeed,
                _shipStatsData.BulletFlyingSpeed, 
                _shipStatsData.RotationSpeed);
            
            return ship;
        }
    }
}