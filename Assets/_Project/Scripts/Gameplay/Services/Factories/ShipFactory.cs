using _Project.Scripts.Gameplay.Characters.Base;
using UnityEngine.AddressableAssets;
using Zenject;

namespace _Project.Scripts.Gameplay.Services.Factories
{
    public class ShipFactory : PlaceholderFactory<Ship.Ship>
    {
        private readonly AssetReference _shipPrefabReference;
        private readonly CharacterCreator _characterCreator;

        public ShipFactory(CharacterCreator characterCreator, AssetReference shipPrefabReference)
        {
            _characterCreator = characterCreator;
            _shipPrefabReference = shipPrefabReference;
        }

        public override Ship.Ship Create() =>
            _characterCreator.CreateGameLoopCharacter<Ship.Ship>(_shipPrefabReference);
    }
}