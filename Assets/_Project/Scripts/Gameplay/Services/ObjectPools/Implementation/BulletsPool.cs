using System;
using _Project.Scripts.Gameplay.Characters;
using _Project.Scripts.Gameplay.Characters.Base;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.Gameplay.Services.ObjectPools
{
    public class BulletsPool : ItemsWithIdAndParameterPool<Bullet, string, Vector2>
    {
        public BulletsPool(
            AssetReference assetReference,
            CharacterCreator characterCreator) :
            base(
                () => characterCreator.CreateGameLoopCharacter<Bullet>(assetReference), 
                () => Guid.NewGuid().ToString(),
                (bullet, position) => bullet.SetPosition(position),
                true)
        {
        }
    }
}