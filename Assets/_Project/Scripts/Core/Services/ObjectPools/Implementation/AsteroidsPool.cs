using System;
using _Project.Scripts.Core.Enemies.Asteroids;
using _Project.Scripts.Core.Services.Repositories;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core.Services.ObjectPools
{
    public class AsteroidsPool : ItemsWithIdAndParameterPool<Asteroid, string, Vector2>
    {
        public AsteroidsPool(
            [Inject(Id = "AsteroidsFactory")]IFactory<Asteroid> factory,
            CharactersRepository repository) : 
            base(
                factory, 
                () => Guid.NewGuid().ToString(),
                (asteroid, parameter) =>
                {
                    asteroid.SetPosition(parameter);
                    repository.RegisterAsteroid(asteroid);
                },
                true)
        {
        }
    }
}