using _Project.Scripts.Core.GameLoopSystem;
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

        public override Bullet Create()
        {
            Bullet bullet = _instantiator
                .InstantiatePrefab(_bulletPrefab)
                .GetComponent<Bullet>();
            bullet.Initialize(
                _gameLoopRegisterController.Register(
                    new BulletMovement(bullet.gameObject.GetComponent<Rigidbody2D>())));
            return bullet;
        }
    }
}
