using System;
using _Project.Scripts.Gameplay.Characters;
using _Project.Scripts.Gameplay.Characters.Base;
using _Project.Scripts.Gameplay.Services.Repositories;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Services.ObjectPools
{
    public class BulletsPool : ItemsWithIdAndParameterPool<Bullet, string, Vector2>
    {
        public BulletsPool(
            Bullet prefab,
            CharacterCreator characterCreator) :
            base(
                () => characterCreator.CreateGameLoopCharacter(prefab), 
                () => Guid.NewGuid().ToString(),
                (bullet, position) => bullet.SetPosition(position),
                true)
        {
        }
    }
}