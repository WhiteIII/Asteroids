using _Project.Scripts.Core.Enemies.Asteroids;
using _Project.Scripts.Core.Services.ObjectPools;
using _Project.Scripts.Data;
using UnityEngine;

namespace _Project.Scripts.Core.Services.Spawners
{
    public class AsteroidsSpawner
    {
        private readonly AsteroidsPool _asteroidsPool;
        private readonly SpawnPositionHelper _positionHelper;
        private readonly AsteroidsData _asteroidsData;
        
        public void Spawn()
        {
            Asteroid asteroid = _asteroidsPool.Get();
            Vector2 spawnPosition = _positionHelper.GetSpawnPosition();
            asteroid.SetPosition(spawnPosition);
            asteroid.SendAsteroidOnDirection(
                GetDirection(
                    spawnPosition),
                Random.Range(_asteroidsData.RandomSpeedFrom, _asteroidsData.RandomSpeedTo));
        }

        private Vector2 GetDirection(Vector2 spawnPosition) =>
            Vector2.zero - new Vector2(
                spawnPosition.x + Random.Range(_asteroidsData.DirectionDeviationFrom, _asteroidsData.DirectionDeviationTo), 
                spawnPosition.y + Random.Range(_asteroidsData.DirectionDeviationFrom, _asteroidsData.DirectionDeviationTo));
    }
}
