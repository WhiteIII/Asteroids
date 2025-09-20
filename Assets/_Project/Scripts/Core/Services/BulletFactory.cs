using _Project.Scripts.Core.Services.GameCycle;
using _Project.Scripts.Core.ShootingSystem;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core.Services
{
    public class BulletFactory : PlaceholderFactory<string, Transform, Bullet>
    {
        private readonly GameObject _bulletPrefab;
        private readonly IGameCycleRegisterController _gameCycleRegisterController;
        private readonly IInstantiator _instantiator;

        public BulletFactory(
            GameObject bulletPrefab,
            IGameCycleRegisterController gameCycleRegisterController,
            IInstantiator instantiator)
        {
            _bulletPrefab = bulletPrefab;
            _gameCycleRegisterController = gameCycleRegisterController;
            _instantiator = instantiator;
        }

        public override Bullet Create(string id, Transform parent)
        {
            GameObject bulletGameObject = _instantiator.InstantiatePrefab(_bulletPrefab);
            bulletGameObject.transform.SetParent(parent);
            return _instantiator.Instantiate<Bullet>(
                new object[]
                {
                    _gameCycleRegisterController.Register(new BulletMovement(
                        bulletGameObject.GetComponent<Rigidbody2D>(),
                        bulletGameObject.transform)),
                    bulletGameObject,
                    new BulletAttackController(bulletGameObject.GetComponent<CollisionHandler>()),
                    id
                });
        }
    }
}