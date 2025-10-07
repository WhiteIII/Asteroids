using _Project.Scripts.Gameplay.GameLoopSystem;
using _Project.Scripts.Gameplay.Services.Repositories;

namespace _Project.Scripts.Gameplay.Services.Spawners
{
    public class SpawnerControllerCreator<T>
        where T : ISpawner
    {
        private readonly SpawnersRepository _repository;
        private readonly IGameLoopCreator _gameLoopCreatorCreator;
        private readonly float _spawnDelay;
        private readonly T _spawner;

        public SpawnerControllerCreator(
            SpawnersRepository repository,
            IGameLoopCreator gameLoopCreatorCreator, 
            T spawner, 
            float spawnDelay)
        {
            _repository = repository;
            _gameLoopCreatorCreator = gameLoopCreatorCreator;
            _spawner = spawner;
            _spawnDelay = spawnDelay;
        }

        public SpawnerController Create() =>
            _gameLoopCreatorCreator.Create<SpawnerController>(_repository.Register(_spawner), _spawnDelay);
    }
}