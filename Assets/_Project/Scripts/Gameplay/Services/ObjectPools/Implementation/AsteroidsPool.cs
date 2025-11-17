using System;
using _Project.Scripts.Data;
using _Project.Scripts.Gameplay.Characters;
using _Project.Scripts.Gameplay.Characters.Base;
using _Project.Scripts.Gameplay.Services.Repositories;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;


namespace _Project.Scripts.Gameplay.Services.ObjectPools
{
    public class AsteroidsPool : ItemsWithIdAndParameterPool<Asteroid, string, Vector2>
    {
        private const string ID = "Asteroid";
        
        public AsteroidsPool(
            CharacterCreator characterCreator,
            SmallAsteroidsPool smallAsteroidsPool,
            AsteroidsData asteroidsData) : 
            base(
                async () =>
                {
                    Asteroid asteroid = await characterCreator.CreateGameLoopCharacter<Asteroid>(ID);
                    asteroid.SetupAsteroid(async () => {
                        int spawnCount = Random.Range(
                            asteroidsData.SpawnedSmallAsteroidsOnDeadCountForm, 
                            asteroidsData.SpawnedSmallAsteroidsOnDeadCountTo + 1);

                        for (int i = 0; i < spawnCount; i++)
                        {
                            Asteroid smallAsteroid = await smallAsteroidsPool.Get(asteroid.Position.CurrentValue);
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
                (asteroid, spawnPosition) => asteroid.SetPosition(spawnPosition),
                true)
        {
        }
    }
}