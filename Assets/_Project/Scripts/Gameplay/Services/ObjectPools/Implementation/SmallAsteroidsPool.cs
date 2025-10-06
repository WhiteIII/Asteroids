using System;
using _Project.Scripts.Gameplay.Characters;
using _Project.Scripts.Gameplay.Characters.Base;
using _Project.Scripts.Gameplay.Services.Repositories;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Services.ObjectPools
{
    public class SmallAsteroidsPool : ItemsWithIdAndParameterPool<Asteroid, string, Vector2>
    {
        public SmallAsteroidsPool(
            CharacterCreator creator,
            Asteroid prefab) : 
            base(
                () => creator.CreateGameLoopCharacter(prefab), 
                () => Guid.NewGuid().ToString(),
                (asteroid, parameter) => asteroid.SetPosition(parameter),
                true)
        {
        }
    }
}