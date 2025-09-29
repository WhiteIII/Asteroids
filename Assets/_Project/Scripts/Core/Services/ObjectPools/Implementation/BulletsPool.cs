using System;
using _Project.Scripts.Core.ShootingSystem;
using Zenject;

namespace _Project.Scripts.Core.Services.ObjectPools
{
    public class BulletsPool : ItemsWithIdPool<Bullet, string>
    {
        public BulletsPool(IFactory<Bullet> factory) : base(factory, () => Guid.NewGuid().ToString())
        {
        }
    }
}