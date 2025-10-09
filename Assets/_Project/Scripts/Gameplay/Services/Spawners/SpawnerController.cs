using _Project.Scripts.Gameplay.Services.ObjectPools.Base;
using Zenject;
using static UnityEngine.Mathf;
using static UnityEngine.Time;

namespace _Project.Scripts.Gameplay.Services.Spawners
{
    public class SpawnerController<T> : ISpawnerController, ITickable
        where T : ISpawner
    {
        private readonly T _spawner;
        private readonly float _spawnCooldown;
        
        private float _currentSpawnCooldown;
        private bool _isActive;
        
        private bool InCooldown => _currentSpawnCooldown > .1f;
                
        public SpawnerController(T spawner, float spawnCooldown)
        {
            _spawner = spawner;
            _spawnCooldown = spawnCooldown;
        }
        
        public void Tick()
        {
            if (_isActive == false)
                return;
            
            _currentSpawnCooldown = Max(_currentSpawnCooldown - deltaTime, 0);
            
            if (InCooldown)
                return;
            
            _spawner.Spawn();
            _currentSpawnCooldown = _spawnCooldown;
        }
        
        public void Enable() => 
            _isActive = true;
        
        public void Disable() => 
            _isActive = false;
    }

    public interface ISpawnerController : IEnableAndDisableItem
    {
        
    }
}