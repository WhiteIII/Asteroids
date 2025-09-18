using UnityEngine;

namespace _Project.Scripts.Core.ShootingSystem
{
    internal class Bullet
    {
        private readonly BulletMovement _bulletMovement;
        private readonly GameObject _bulletGameObject;
        
        public void Enable() => 
            _bulletGameObject.SetActive(true);
        
        public void Disable() =>
            _bulletGameObject.SetActive(false);
        
        public void SendBulletInTheDirection(Vector2 direction) =>
            _bulletMovement.SetDirection(direction);
    }
}
