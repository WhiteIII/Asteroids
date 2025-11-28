using System;
using _Project.Scripts.Gameplay.Characters.Base;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace _Project.Scripts.Gameplay.Services.Factories
{
    public class ShipFactory : PlaceholderFactory<ShipSpawnArgs, Ship.Ship>
    {
        private readonly AssetReference _shipPrefabReference;
        private readonly CharacterCreator _characterCreator;

        public ShipFactory(CharacterCreator characterCreator, AssetReference shipPrefabReference)
        {
            _characterCreator = characterCreator;
            _shipPrefabReference = shipPrefabReference;
        }

        public override Ship.Ship Create(ShipSpawnArgs spawnArgs)
        {
            Ship.Ship ship = _characterCreator.CreateGameLoopCharacter<Ship.Ship>(_shipPrefabReference);
            ship.SetPosition(spawnArgs.Position);
            ship.SetRotation(spawnArgs.Rotation);
            ship.SetOnDeadEvent(spawnArgs.OnDead);
            return ship;
        }
    }

    public struct ShipSpawnArgs
    {
        public Vector2 Position;
        public Quaternion Rotation;
        public Action OnDead;
    }
}