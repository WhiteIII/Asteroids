using _Project.Scripts.Gameplay.Characters.Base;
using Zenject;

namespace _Project.Scripts.Gameplay.Services.Factories
{
    public class ShipFactory : PlaceholderFactory<Ship.Ship>
    {
        private readonly Ship.Ship _shipPrefab;
        private readonly CharacterCreator _characterCreator;

        public ShipFactory(Ship.Ship shipPrefab, CharacterCreator characterCreator)
        {
            _shipPrefab = shipPrefab;
            _characterCreator = characterCreator;
        }

        public override Ship.Ship Create()
        {
            Ship.Ship ship = _characterCreator.CreateGameLoopCharacter(_shipPrefab);
            
            return ship;
        }
    }
}