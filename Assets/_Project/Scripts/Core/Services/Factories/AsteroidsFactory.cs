using _Project.Scripts.Core.Enemies.Asteroids;
using _Project.Scripts.Core.GameLoopSystem;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core.Services.Factories
{
    public class AsteroidsFactory : PlaceholderFactory<Asteroid>
    {
        private readonly GameObject _prefab; 
        private readonly IInstantiator _instantiator;
        private readonly IGameLoopRegisterController _gameLoopRegisterController; 

        public AsteroidsFactory(
            GameObject prefab,
            IInstantiator instantiator,
            IGameLoopRegisterController gameLoopRegisterController)
        {
            _prefab = prefab;
            _instantiator = instantiator;
            _gameLoopRegisterController = gameLoopRegisterController;
        }

        public override Asteroid Create() => 
                _instantiator
                    .InstantiatePrefab(_prefab)
                    .GetComponent<Asteroid>();
    }
}