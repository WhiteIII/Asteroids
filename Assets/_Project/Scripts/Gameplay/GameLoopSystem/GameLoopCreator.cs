using _Project.Scripts.Common;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace _Project.Scripts.Gameplay.GameLoopSystem
{
    public class GameLoopCreator : IGameLoopCreator
    {
        private readonly IGameLoopRegisterController _gameLoopRegisterController;
        private readonly IInstantiator _instantiator;
        private readonly LocalAssetProvider _localAssetProvider;

        public GameLoopCreator(
            IGameLoopRegisterController gameLoopRegisterController, 
            IInstantiator instantiator, 
            LocalAssetProvider localAssetProvider)
        {
            _gameLoopRegisterController = gameLoopRegisterController;
            _instantiator = instantiator;
            _localAssetProvider = localAssetProvider;
        }

        public T Create<T>(AssetReference assetReference) where T : MonoBehaviour, IGameLoopObject =>
            RegisterObject(
                _instantiator.InstantiatePrefab(
                    _localAssetProvider.GetAsset<GameObject>(assetReference)).GetComponent<T>());

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
        T Create<T>(AssetReference assetReference) where T : MonoBehaviour, IGameLoopObject;
    }
}