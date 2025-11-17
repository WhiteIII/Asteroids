using System;
using _Project.Scripts.Data;
using _Project.Scripts.Gameplay.Characters;
using _Project.Scripts.Gameplay.Services.ObjectPools;
using Cysharp.Threading.Tasks;
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

        public async void Spawn()
        {
            int poolNumber = Random.Range(0, 2);
            Vector2 spawnPosition = _positionHelper.GetSpawnPosition();
            UniTask<Asteroid> asteroidCreateTask = poolNumber switch
            {
                0 => GetRandomAsteroid(() => _asteroidsPool.Get(spawnPosition), _asteroidsData.Points, spawnPosition),
                1 => GetRandomAsteroid(() => _smallAsteroidsPool.Get(spawnPosition), _asteroidsData.SmallAsteroidsPoints, spawnPosition),
                _ => GetRandomAsteroid(() => _asteroidsPool.Get(spawnPosition), _asteroidsData.Points,  spawnPosition)
            };
            await asteroidCreateTask;
        }

        private Vector2 GetDirection(Vector2 spawnPosition) =>
            (Vector2.zero - new Vector2(
                spawnPosition.x + Random.Range(_asteroidsData.DirectionDeviationFrom, _asteroidsData.DirectionDeviationTo), 
                spawnPosition.y + Random.Range(_asteroidsData.DirectionDeviationFrom, _asteroidsData.DirectionDeviationTo)))
            .normalized;

        private async UniTask<Asteroid> GetRandomAsteroid(Func<UniTask<Asteroid>> spawnMethod, int points, Vector2 spawnPosition)
        {
            Asteroid asteroid = await spawnMethod();
            asteroid.SetPoints(points);
            asteroid.SendAsteroidOnDirection(
                GetDirection(spawnPosition),
                Random.Range(_asteroidsData.RandomSpeedFrom, _asteroidsData.RandomSpeedTo));
            return asteroid;
        }
    }
}