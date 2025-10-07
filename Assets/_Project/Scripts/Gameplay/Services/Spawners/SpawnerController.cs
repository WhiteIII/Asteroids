using _Project.Scripts.Gameplay.GameLoopSystem;
using static UnityEngine.Mathf;
using static UnityEngine.Time;

namespace _Project.Scripts.Gameplay.Services.Spawners
{
    public class SpawnerController : IUpdatable
    {
        private readonly ISpawner _spawner;
        private readonly float _spawnCooldown;
        
        private float _currentSpawnCooldown;

        private bool InCooldown => _currentSpawnCooldown > .1f;
        
        public SpawnerController(ISpawner spawner, float spawnCooldown)
        {
            _spawner = spawner;
            _spawnCooldown = spawnCooldown;
        }
        
        public void GameLoopUpdate()
        {
            _currentSpawnCooldown = Max(_currentSpawnCooldown - deltaTime, 0);
            
            if (InCooldown)
                return;
            
            _spawner.Spawn();
            _currentSpawnCooldown = _spawnCooldown;
        }
    }
}