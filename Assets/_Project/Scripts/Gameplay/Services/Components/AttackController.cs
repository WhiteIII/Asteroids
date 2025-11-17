using System;
using _Project.Scripts.Gameplay.Characters;
using _Project.Scripts.Gameplay.Services.Components.View;
using _Project.Scripts.Gameplay.Services.ObjectPools;
using _Project.Scripts.Gameplay.Services.Targets;
using _Project.Scripts.Gameplay.Services.Targets.Base;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using static UnityEngine.Mathf;

namespace _Project.Scripts.Gameplay.Ship
{
    public class AttackController : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private SfxController _shootSfx;
        
        private BulletsPool _bulletsPool;
        private float _bulletFlyingSpeed;

        [Inject] private void Construct(BulletsPool bulletsPool) => 
            _bulletsPool = bulletsPool;
        
        public void Initialize(float bulletFlyingSpeed) => 
            _bulletFlyingSpeed = bulletFlyingSpeed;

        public async void Shoot<T>(Vector2 direction) 
            where T : IKillableTarget
        {
            _shootSfx.PlayAudioClipAsync().Forget();
            Bullet bullet = await CreateAndSetupBullet(direction); 
            bullet.SetIgnoreTarget<T>();
        }

        public async void Shoot<T1, T2>(Vector2 direction) 
            where T1 : IKillableTarget where T2 : IKillableTarget
        {
            _shootSfx.PlayAudioClipAsync().Forget();
            Bullet bullet = await CreateAndSetupBullet(direction); 
            bullet.SetIgnoreTarget<T1, T2>();
        }

        private async UniTask<Bullet> CreateAndSetupBullet(Vector2 direction)
        {
            Bullet bullet = await _bulletsPool.Get(_spawnPoint.position);
            bullet.SetRotation(GetRotation(direction));
            bullet.SetDirection(direction);
            bullet.SetFlySpeed(_bulletFlyingSpeed);
            return bullet;
        }
        
        private Quaternion GetRotation(Vector3 direction)
        {
            float angle = Atan2(direction.y, direction.x) * Rad2Deg;
            return Quaternion.Euler(0, 0, angle);
        }
    }
}