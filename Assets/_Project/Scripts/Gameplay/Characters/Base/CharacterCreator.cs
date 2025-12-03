using _Project.Scripts.Common;
using _Project.Scripts.Common.Services.AssetsManagement;
using _Project.Scripts.Gameplay.GameLoopSystem;
using _Project.Scripts.Gameplay.Services.Repositories;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace _Project.Scripts.Gameplay.Characters.Base
{
    public class CharacterCreator
    {
        private readonly IInstantiator _instantiator;
        private readonly IGameLoopCreator _gameLoopCreator;
        private readonly CharactersRepository _charactersRepository;
        private readonly LocalAssetsProvider _localAssetsProvider;

        public CharacterCreator(
            IInstantiator instantiator, 
            IGameLoopCreator gameLoopCreator,
            CharactersRepository charactersRepository, 
            LocalAssetsProvider localAssetsProvider)
        {
            _instantiator = instantiator;
            _gameLoopCreator = gameLoopCreator;
            _charactersRepository = charactersRepository;
            _localAssetsProvider = localAssetsProvider;
        }

        public T CreateNonGameLoopCharacter<T>(AssetReference assetReference) where T : Character =>
            _charactersRepository.Register(
                _instantiator.InstantiatePrefab(
                    _localAssetsProvider.GetAsset<GameObject>(assetReference)).GetComponent<T>());

        public T CreateGameLoopCharacter<T>(AssetReference assetReference) where T : Character, IGameLoopObject =>
            _charactersRepository.Register(_gameLoopCreator.Create<T>(assetReference));
    }
}