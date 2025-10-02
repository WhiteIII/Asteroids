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

        public T Create<T>(GameObject prefab) 
            where T : MonoBehaviour, IGameLoopObject
        {
            T updatableComponent = _instantiator.InstantiatePrefab(prefab).GetComponent<T>();

            if (updatableComponent is IUpdatable updatable)
                _gameLoopRegisterController.Register(updatable);
            else if (updatableComponent is IInitializableUpdatableObject updatableObject)
                _gameLoopRegisterController.RegisterInitializableObject(updatableObject);

            return updatableComponent;
        }
    }

    public interface IGameLoopCreator
    {
        T Create<T>(GameObject prefab) where T : MonoBehaviour, IGameLoopObject;
    }
}