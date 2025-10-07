using _Project.Scripts.Gameplay.Services.ObjectPools;

namespace _Project.Scripts.Gameplay.Services.Spawners
{
    public class UfoSpawner : ISpawner
    {
        private readonly UfoPool _ufoPool;
        private readonly ISpawnPositionHelper _spawnPositionHelper;

        public UfoSpawner(UfoPool ufoPool, ISpawnPositionHelper spawnPositionHelper)
        {
            _ufoPool = ufoPool;
            _spawnPositionHelper = spawnPositionHelper;
        }

        public void Spawn() =>
            _ufoPool.Get(_spawnPositionHelper.GetSpawnPosition());
    }
}