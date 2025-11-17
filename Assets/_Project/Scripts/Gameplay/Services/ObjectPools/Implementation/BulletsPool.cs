using System;
using _Project.Scripts.Gameplay.Characters;
using _Project.Scripts.Gameplay.Characters.Base;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Services.ObjectPools
{
    public class BulletsPool : ItemsWithIdAndParameterPool<Bullet, string, Vector2>
    {
        private const string ID = "Bullet";
        
        public BulletsPool(
            CharacterCreator characterCreator) :
            base(
                () => characterCreator.CreateGameLoopCharacter<Bullet>(ID), 
                () => Guid.NewGuid().ToString(),
                (bullet, position) => bullet.SetPosition(position),
                true)
        {
        }
    }
}