using System;
using Zenject;
using static UnityEngine.Mathf;
using static UnityEngine.Time;

namespace _Project.Scripts.Core.Services.Spawners
{
    public class SpawnersController : ITickable
    {
        private readonly AsteroidsSpawner _asteroidsSpawner;
        private readonly float _asteroidsSpawnCooldown;
        
        private float _currentAsteroidsSpawnCooldown;

        private bool InCooldown => _currentAsteroidsSpawnCooldown > .1f;
        
        public SpawnersController(AsteroidsSpawner asteroidsSpawner, float asteroidsSpawnCooldown)
        {
            _asteroidsSpawner = asteroidsSpawner;
            _asteroidsSpawnCooldown = asteroidsSpawnCooldown;
        }
        
        public void Tick()
        {
            _currentAsteroidsSpawnCooldown = Max(_currentAsteroidsSpawnCooldown - deltaTime, 0);
            
            if (InCooldown)
                return;
            
            _asteroidsSpawner.Spawn();
            _currentAsteroidsSpawnCooldown = _asteroidsSpawnCooldown;
        }
    }
}