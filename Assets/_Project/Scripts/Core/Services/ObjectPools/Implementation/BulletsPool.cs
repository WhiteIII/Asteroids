using System;
using _Project.Scripts.Core.Services.Repositories;
using _Project.Scripts.Core.ShootingSystem;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core.Services.ObjectPools
{
    public class BulletsPool : ItemsWithIdAndParameterPool<Bullet, string, Vector2>
    {
        public BulletsPool(
            CharactersRepository charactersRepository, 
            IFactory<Bullet> factory) :
            base(
                factory, 
                () => Guid.NewGuid().ToString(),
                (bullet, position) =>
                {
                    bullet.SetPosition(position);
                    charactersRepository.RegisterBullet(bullet);
                })
        {
        }
    }
}