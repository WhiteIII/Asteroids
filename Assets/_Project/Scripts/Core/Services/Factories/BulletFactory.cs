using _Project.Scripts.Core.GameLoopSystem;
using _Project.Scripts.Core.Services.Components;
using _Project.Scripts.Core.ShootingSystem;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core.Services.Factories
{
    public class BulletFactory : PlaceholderFactory<Bullet>
    {
        private readonly GameObject _bulletPrefab;
        private readonly IGameLoopRegisterController _gameLoopRegisterController;
        private readonly IInstantiator _instantiator;

        public BulletFactory(
            GameObject bulletPrefab,
            IGameLoopRegisterController gameLoopRegisterController, 
            IInstantiator instantiator)
        {
            _bulletPrefab = bulletPrefab;
            _gameLoopRegisterController = gameLoopRegisterController;
            _instantiator = instantiator;
        }

        public override Bullet Create() =>
            _gameLoopRegisterController.RegisterInitializableObject(_instantiator
                .InstantiatePrefab(_bulletPrefab)
                .GetComponent<Bullet>());
    }
}
