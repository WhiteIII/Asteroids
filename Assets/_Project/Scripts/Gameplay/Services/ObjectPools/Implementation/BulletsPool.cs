using System;
using _Project.Scripts.Gameplay.Enemies;
using _Project.Scripts.Gameplay.Services.Repositories;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Services.ObjectPools
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