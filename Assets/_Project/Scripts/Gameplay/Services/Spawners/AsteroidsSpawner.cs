using _Project.Scripts.Core.Enemies;
using _Project.Scripts.Core.Services.ObjectPools;
using _Project.Scripts.Data;
using UnityEngine;

namespace _Project.Scripts.Core.Services.Spawners
{
    public class AsteroidsSpawner
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
                0 => _asteroidsPool.Get(spawnPosition),
                1 => _smallAsteroidsPool.Get(spawnPosition),
                _ => _asteroidsPool.Get(spawnPosition)
            };
                
            asteroid.SendAsteroidOnDirection(
                GetDirection(
                    spawnPosition),
                Random.Range(_asteroidsData.RandomSpeedFrom, _asteroidsData.RandomSpeedTo));
        }

        private Vector2 GetDirection(Vector2 spawnPosition) =>
            (Vector2.zero - new Vector2(
                spawnPosition.x + Random.Range(_asteroidsData.DirectionDeviationFrom, _asteroidsData.DirectionDeviationTo), 
                spawnPosition.y + Random.Range(_asteroidsData.DirectionDeviationFrom, _asteroidsData.DirectionDeviationTo)))
            .normalized;
    }
}