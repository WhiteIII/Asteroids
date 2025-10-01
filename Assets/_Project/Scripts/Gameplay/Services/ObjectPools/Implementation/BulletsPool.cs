using System;
using _Project.Scripts.Core.Enemies;
using _Project.Scripts.Core.Services.Repositories;
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
                (bullet, position) => bullet.SetPosition(position),
                true)
        {
        }
    }
}