using System;
using _Project.Scripts.Gameplay.Characters.Base;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;
using _Project.Scripts.Gameplay.ShipBase;

namespace _Project.Scripts.Gameplay.Services.Factories
{
    public class ShipFactory : PlaceholderFactory<ShipSpawnArgs, Ship>
    {
        private readonly AssetReference _shipPrefabReference;
        private readonly CharacterCreator _characterCreator;

        public ShipFactory(CharacterCreator characterCreator, AssetReference shipPrefabReference)
        {
            _characterCreator = characterCreator;
            _shipPrefabReference = shipPrefabReference;
        }

        public override Ship Create(ShipSpawnArgs spawnArgs)
        {
            Ship ship = _characterCreator.CreateGameLoopCharacter<Ship>(_shipPrefabReference);
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