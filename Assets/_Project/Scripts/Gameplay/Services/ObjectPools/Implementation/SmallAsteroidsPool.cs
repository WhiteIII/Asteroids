using System;
using _Project.Scripts.Gameplay.Characters;
using _Project.Scripts.Gameplay.Characters.Base;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Services.ObjectPools
{
    public class SmallAsteroidsPool : ItemsWithIdAndParameterPool<Asteroid, string, Vector2>
    {
        public SmallAsteroidsPool(
            CharacterCreator creator,
            Asteroid prefab) : 
            base(
                () =>
                {
                    Asteroid asteroid = creator.CreateGameLoopCharacter(prefab);
                    asteroid.SetupAsteroid();
                    return asteroid;
                }, 
                () => Guid.NewGuid().ToString(),
                (asteroid, parameter) => asteroid.SetPosition(parameter),
                true)
        {
        }
    }
}