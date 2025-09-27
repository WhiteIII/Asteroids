using _Project.Scripts.Core.Enemies.Asteroids;
using _Project.Scripts.Core.Services.ObjectPools;
using _Project.Scripts.Core.Services.Repositories;
using _Project.Scripts.Data;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core.Services.Spawners
{
    public class AsteroidsSpawner
    {
        private readonly AsteroidsPool _asteroidsPool;
        private readonly SmallAsteroidsPool _smallAsteroidsPool;
        private readonly ISpawnPositionHelper _positionHelper;
        private readonly AsteroidsData _asteroidsData;
        private readonly CharactersRepository _charactersRepository;
        
        public AsteroidsSpawner(
            AsteroidsPool asteroidsPool,
            ISpawnPositionHelper positionHelper,
            AsteroidsData asteroidsData,
            SmallAsteroidsPool smallAsteroidsPool,
            CharactersRepository charactersRepository)
        {
            _asteroidsPool = asteroidsPool;
            _positionHelper = positionHelper;
            _asteroidsData = asteroidsData;
            _smallAsteroidsPool = smallAsteroidsPool;
            _charactersRepository = charactersRepository;
        }

        public void Spawn()
        {
            int poolNumber = Random.Range(0, 2);
            Asteroid asteroid = poolNumber switch
            {
                0 => GetAsteroid(),
                1 => GetSmallAsteroid(),
                _ => GetAsteroid()
            };
                
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

        private Asteroid GetAsteroid()
        {
            Asteroid asteroid = _asteroidsPool.Get();
            _charactersRepository.RegisterAsteroid(asteroid);
            return asteroid;
        }

        public Asteroid GetSmallAsteroid()
        {
            Asteroid asteroid = _smallAsteroidsPool.Get();
            _charactersRepository.RegisterSmallAsteroid(asteroid);
            return asteroid;
        }
    }
}