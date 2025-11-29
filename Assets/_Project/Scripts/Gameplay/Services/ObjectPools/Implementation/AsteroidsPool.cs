using System;
using _Project.Scripts.Data;
using _Project.Scripts.Gameplay.Characters;
using _Project.Scripts.Gameplay.Characters.Base;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Random = UnityEngine.Random;


namespace _Project.Scripts.Gameplay.Services.ObjectPools
{
    public class AsteroidsPool : ItemsWithIdAndParameterPool<Asteroid, string, Vector2>
    {
        public AsteroidsPool(
            CharacterCreator characterCreator,
            AssetReference asteroidAssetReference,
            SmallAsteroidsPool smallAsteroidsPool,
            AsteroidsData asteroidsData) : 
            base(
                () =>
                {
                    Asteroid asteroid = characterCreator.CreateGameLoopCharacter<Asteroid>(asteroidAssetReference);
                    asteroid.SetupAsteroid(() => {
                        int spawnCount = Random.Range(
                            asteroidsData.SpawnedSmallAsteroidsOnDeadCountForm, 
                            asteroidsData.SpawnedSmallAsteroidsOnDeadCountTo + 1);

                        for (int i = 0; i < spawnCount; i++)
                        {
                            Asteroid smallAsteroid = smallAsteroidsPool.Get(asteroid.Position.CurrentValue);
                            smallAsteroid.SendAsteroidOnDirection(
                                new Vector2(
                                    asteroid.Direction.x + Random.Range(
                                        asteroidsData.DirectionDeviationFrom, 
                                        asteroidsData.DirectionDeviationTo), 
                                    asteroid.Direction.y + Random.Range(
                                        asteroidsData.DirectionDeviationFrom, 
                                        asteroidsData.DirectionDeviationTo)).normalized,
                                Random.Range(asteroidsData.RandomSpeedFrom,  asteroidsData.RandomSpeedTo));
                        }});
                    return asteroid;
                }, 
                () => Guid.NewGuid().ToString(),
                (asteroid, spawnPosition) =>
                {
                    asteroid.Revive();
                    asteroid.SetPosition(spawnPosition);
                },
                true)
        {
        }
    }
}