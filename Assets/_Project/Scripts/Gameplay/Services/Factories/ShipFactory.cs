using _Project.Scripts.Gameplay.GameLoopSystem;
using _Project.Scripts.Gameplay.Services.Repositories;
using Zenject;

namespace _Project.Scripts.Gameplay.Services.Factories
{
    public class ShipFactory : PlaceholderFactory<Ship.Ship>
    {
        private readonly Ship.Ship _shipPrefab;
        private readonly IGameLoopCreator _creator;
        private readonly CharactersRepository _repository;

        public ShipFactory(
            Ship.Ship shipPrefab,
            IGameLoopCreator creator,
            CharactersRepository repository)
        {
            _shipPrefab = shipPrefab;
            _creator = creator;
            _repository = repository;
        }

        public override Ship.Ship Create()
        {
            Ship.Ship ship = _repository.RegisterShip(_creator.Create(_shipPrefab));
            ship.Initialize();
            
            return ship;
        }
    }
}