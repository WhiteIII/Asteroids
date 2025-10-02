using System;
using _Project.Scripts.Gameplay.Enemies;
using _Project.Scripts.Gameplay.Services.Repositories;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Services.ObjectPools
{
    public class SmallAsteroidsPool : ItemsWithIdAndParameterPool<Asteroid, string, Vector2>
    {
        public SmallAsteroidsPool(
            [Inject(Id = "SmallAsteroidsFactory")]IFactory<Asteroid> factory,
            CharactersRepository repository) : 
            base(
                factory, 
                () => Guid.NewGuid().ToString(),
                (asteroid, parameter) => asteroid.SetPosition(parameter),
                true)
        {
        }
    }
}