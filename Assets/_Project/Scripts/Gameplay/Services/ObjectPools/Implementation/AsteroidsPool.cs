using System;
using _Project.Scripts.Core.Enemies;
using _Project.Scripts.Core.Services.Repositories;
using _Project.Scripts.Data;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;


namespace _Project.Scripts.Core.Services.ObjectPools
{
    public class AsteroidsPool : ItemsWithIdAndParameterPool<Asteroid, string, Vector2>
    {
        public AsteroidsPool(
            [Inject(Id = "AsteroidsFactory")]IFactory<Asteroid> factory,
            CharactersRepository repository,
            SmallAsteroidsPool smallAsteroidsPool,
            AsteroidsData asteroidsData) : 
            base(
                factory, 
                () => Guid.NewGuid().ToString(),
                (asteroid, spawnPosition) => asteroid.SetPosition(spawnPosition),
                true,
                (x) =>
                {
                    int spawnCount = Random.Range(
                        asteroidsData.SpawnedSmallAsteroidsOnDeadCountForm, 
                        asteroidsData.SpawnedSmallAsteroidsOnDeadCountTo + 1);

                    for (int i = 0; i < spawnCount; i++)
                    {
                        Asteroid asteroid = smallAsteroidsPool.Get(x.Position);
                        asteroid.SendAsteroidOnDirection(
                            new Vector2(
                                x.Direction.x + Random.Range(
                                    asteroidsData.DirectionDeviationFrom, 
                                    asteroidsData.DirectionDeviationTo), 
                                x.Direction.y + Random.Range(
                                    asteroidsData.DirectionDeviationFrom, 
                                    asteroidsData.DirectionDeviationTo)).normalized,
                            Random.Range(asteroidsData.RandomSpeedFrom,  asteroidsData.RandomSpeedTo));
                    }
                })
        {
        }
    }
}