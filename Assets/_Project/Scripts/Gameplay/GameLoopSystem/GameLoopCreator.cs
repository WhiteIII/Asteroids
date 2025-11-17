using _Project.Scripts.Common;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.GameLoopSystem
{
    public class GameLoopCreator : IGameLoopCreator
    {
        private readonly IGameLoopRegisterController _gameLoopRegisterController;
        private readonly DiContainer _container;
        private readonly LocalAssetProvider _localAssetProvider;

        public GameLoopCreator(
            IGameLoopRegisterController gameLoopRegisterController, 
            DiContainer container,
            LocalAssetProvider localAssetProvider)
        {
            _gameLoopRegisterController = gameLoopRegisterController;
            _container = container;
            _localAssetProvider = localAssetProvider;
        }

        public async UniTask<T> Create<T>(string id)
            where T : MonoBehaviour, IGameLoopObject
        {
            T createdObject = await _localAssetProvider.LoadAsync<T>(id);
            _container.Inject(createdObject);
            return RegisterObject(createdObject);
        }
        
        public T Create<T>(params object[] parameters)
            where T : IGameLoopObject
        {
            return RegisterObject(_container.Instantiate<T>(parameters));
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
        UniTask<T> Create<T>(string id) where T : MonoBehaviour, IGameLoopObject;
        T Create<T>(params object[] parameters) where T : IGameLoopObject;
    }
}