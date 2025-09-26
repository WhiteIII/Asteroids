using _Project.Scripts.Core.Enemies.Asteroids;
using _Project.Scripts.Core.Services.Components;
using _Project.Scripts.Core.ShootingSystem;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core.Services.Factories
{
    public class AsteroidsFactory : PlaceholderFactory<Asteroid>
    {
        private readonly GameObject _prefab; 
        private readonly IInstantiator _instantiator;

        public AsteroidsFactory(GameObject prefab, IInstantiator instantiator)
        {
            _prefab = prefab;
            _instantiator = instantiator;
        }

        public override Asteroid Create()
        {
            GameObject asteroidGameObject = _instantiator.InstantiatePrefab(_prefab);
            Asteroid asteroid = asteroidGameObject.GetComponent<Asteroid>();
            asteroid.Initialize(
                new RigidbodyMovement(asteroidGameObject.GetComponent<Rigidbody2D>()),
                asteroidGameObject.GetComponent<CollisionHandler>());
            return asteroid;
        }
    }
}