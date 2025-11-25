using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Services.Spawners;

namespace _Project.Scripts.Gameplay.Services.Repositories
{
    public class SpawnersAndControllersRepository
    {
        private readonly List<ISpawner> _spawners = new();
        private readonly List<ISpawnerController> _spawnerControllers = new();

        public SpawnersAndControllersRepository(ISpawner[] spawners, ISpawnerController[] spawnerControllers)
        {
            foreach (ISpawner spawner in spawners)
                RegisterSpawner(spawner);
            foreach (ISpawnerController controller in spawnerControllers)
                RegisterController(controller);
        }
        
        public void RegisterController(ISpawnerController controller) => 
            _spawnerControllers.Add(controller);

        public void UnregisterController(ISpawnerController controller) =>
            _spawnerControllers.Remove(controller);

        public T RegisterSpawner<T>(T spawner)
            where T : ISpawner
        {
            _spawners.Add(spawner);
            return spawner;
        }

        public void UnregisterSpawner(ISpawner spawner) => 
            _spawners.Remove(spawner);

        public void StartAllSpawners()
        {
            foreach (ISpawnerController controller in _spawnerControllers)
                controller.Enable();
        }
        
        public void StopAllSpawnerControllers()
        {
            foreach (ISpawnerController controller in  _spawnerControllers)
                controller.Disable();
        }
        
        public void Clear()
        {
            StopAllSpawnerControllers();
            foreach (ISpawnerController controller in _spawnerControllers)
            {
                if (controller is IDisposable disposable)
                    disposable.Dispose();
            }
            
            foreach (ISpawner spawner in _spawners)
            {
                if (spawner is IDisposable disposable)
                    disposable.Dispose();
            }
            _spawners.Clear();
            _spawnerControllers.Clear();
        }
    }
}