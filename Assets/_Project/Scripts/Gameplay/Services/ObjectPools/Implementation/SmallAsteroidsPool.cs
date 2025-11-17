using System;
using _Project.Scripts.Gameplay.Characters;
using _Project.Scripts.Gameplay.Characters.Base;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Services.ObjectPools
{
    public class SmallAsteroidsPool : ItemsWithIdAndParameterPool<Asteroid, string, Vector2>
    {
        private const string ID = "SmallAsteroid";
        
        public SmallAsteroidsPool(CharacterCreator creator) : 
            base(
                async () =>
                {
                    Asteroid asteroid = await creator.CreateGameLoopCharacter<Asteroid>(ID);
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