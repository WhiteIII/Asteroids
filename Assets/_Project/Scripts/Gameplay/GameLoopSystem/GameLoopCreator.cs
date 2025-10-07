using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.GameLoopSystem
{
    public class GameLoopCreator : IGameLoopCreator
    {
        private readonly IGameLoopRegisterController _gameLoopRegisterController;
        private readonly IInstantiator _instantiator;

        public GameLoopCreator(
            IGameLoopRegisterController gameLoopRegisterController, 
            IInstantiator instantiator)
        {
            _gameLoopRegisterController = gameLoopRegisterController;
            _instantiator = instantiator;
        }

        public T Create<T>(T prefab)
            where T : MonoBehaviour, IGameLoopObject
        {
            return RegisterObject(_instantiator.InstantiatePrefab(prefab).GetComponent<T>());
        }
        
        public T Create<T>(params object[] parameters)
            where T : IGameLoopObject
        {
            return RegisterObject(_instantiator.Instantiate<T>(parameters));
        }

        public T RegisterObject<T>(T gameLoopObject)
            where T : IGameLoopObject
        {
            if (gameLoopObject is IUpdatable updatable)
                _gameLoopRegisterController.Register(updatable);
            else if (gameLoopObject is IInitializableUpdatableObject initializableUpdatableObject)
                _gameLoopRegisterController.RegisterInitializableObject(initializableUpdatableObject);
            return gameLoopObject;
        }
    }

    public interface IGameLoopCreator
    {
        T RegisterObject<T>(T gameLoopObject) where T : IGameLoopObject;
        T Create<T>(T prefab) where T : MonoBehaviour, IGameLoopObject;
        T Create<T>(params object[] parameters) where T : IGameLoopObject;
    }
}