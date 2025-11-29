using System;
using _Project.Scripts.Gameplay.Characters;
using _Project.Scripts.Gameplay.Characters.Base;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.Gameplay.Services.ObjectPools
{
    public class SmallAsteroidsPool : ItemsWithIdAndParameterPool<Asteroid, string, Vector2>
    {
        public SmallAsteroidsPool(
            CharacterCreator creator,
            AssetReference smallAsteroidAssetReferences) : 
            base(
                () =>
                {
                    Asteroid asteroid = creator.CreateGameLoopCharacter<Asteroid>(smallAsteroidAssetReferences);
                    asteroid.SetupAsteroid();
                    return asteroid;
                }, 
                () => Guid.NewGuid().ToString(),
                (asteroid, parameter) =>
                {
                    asteroid.Revive();
                    asteroid.SetPosition(parameter);
                },
                true)
        {
        }
    }
}