using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Services.Spawners;

namespace _Project.Scripts.Gameplay.Services.Repositories
{
    public class SpawnersRepository
    {
        private readonly List<ISpawner> _spawners = new();
        
        public T Register<T>(T spawner)
            where T : ISpawner
        {
            _spawners.Add(spawner);
            return spawner;
        }

        public void Unregister(ISpawner spawner) => 
            _spawners.Remove(spawner);

        public void Clear()
        {
            foreach (ISpawner spawner in _spawners)
            {
                if (spawner is IDisposable disposable)
                    disposable.Dispose();
            }
            _spawners.Clear();
        }
    }
}