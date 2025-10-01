using System;
using _Project.Scripts.Core.Enemies;
using _Project.Scripts.Core.Services.Repositories;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core.Services.ObjectPools
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