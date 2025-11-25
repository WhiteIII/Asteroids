using System;
using _Project.Scripts.Data;
using _Project.Scripts.Gameplay.Characters;
using _Project.Scripts.Gameplay.Services.ObjectPools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Gameplay.Services.Spawners
{
    public class AsteroidsSpawner : ISpawner
    {
        private readonly AsteroidsPool _asteroidsPool;
        private readonly SmallAsteroidsPool _smallAsteroidsPool;
        private readonly ISpawnPositionHelper _positionHelper;
        private readonly AsteroidsData _asteroidsData;

        public AsteroidsSpawner(
            AsteroidsPool asteroidsPool,
            ISpawnPositionHelper positionHelper,
            AsteroidsData asteroidsData,
            SmallAsteroidsPool smallAsteroidsPool)
        {
            _asteroidsPool = asteroidsPool;
            _positionHelper = positionHelper;
            _asteroidsData = asteroidsData;
            _smallAsteroidsPool = smallAsteroidsPool;
        }

        public void Spawn()
        {
            int poolNumber = Random.Range(0, 2);
            Vector2 spawnPosition = _positionHelper.GetSpawnPosition();
            Asteroid asteroid = poolNumber switch
            {
                0 => GetRandomAsteroid(() => _asteroidsPool.Get(spawnPosition), _asteroidsData.Points),
                1 => GetRandomAsteroid(
                    () => _smallAsteroidsPool.Get(spawnPosition), 
                    _asteroidsData.SmallAsteroidsPoints),
                _ => GetRandomAsteroid(() => _asteroidsPool.Get(spawnPosition), _asteroidsData.Points)
            };
            
            asteroid.SendAsteroidOnDirection(
                GetDirection(spawnPosition),
                Random.Range(_asteroidsData.RandomSpeedFrom, _asteroidsData.RandomSpeedTo));
        }

        private Vector2 GetDirection(Vector2 spawnPosition) =>
            (Vector2.zero - new Vector2(
                spawnPosition.x + Random.Range(_asteroidsData.DirectionDeviationFrom, _asteroidsData.DirectionDeviationTo), 
                spawnPosition.y + Random.Range(_asteroidsData.DirectionDeviationFrom, _asteroidsData.DirectionDeviationTo)))
            .normalized;

        private Asteroid GetRandomAsteroid(Func<Asteroid> spawnMethod, int points)
        {
            Asteroid asteroid = spawnMethod();
            asteroid.SetPoints(points);
            return asteroid;
        }
    }
}