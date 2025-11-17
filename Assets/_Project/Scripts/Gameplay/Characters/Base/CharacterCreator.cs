using _Project.Scripts.Common;
using _Project.Scripts.Gameplay.GameLoopSystem;
using _Project.Scripts.Gameplay.Services.Repositories;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.Scripts.Gameplay.Characters.Base
{
    public class CharacterCreator
    {
        private readonly DiContainer _container;
        private readonly LocalAssetProvider _localAssetProvider;
        private readonly IGameLoopCreator _gameLoopCreator;
        private readonly CharactersRepository _charactersRepository;

        public CharacterCreator(
            IGameLoopCreator gameLoopCreator,
            CharactersRepository charactersRepository, 
            DiContainer container,
            LocalAssetProvider localAssetProvider)
        {
            _gameLoopCreator = gameLoopCreator;
            _charactersRepository = charactersRepository;
            _container = container;
            _localAssetProvider = localAssetProvider;
        }

        public async UniTask<T> CreateNonGameLoopCharacter<T>(string id)
            where T : Character
        {
            T character = await _localAssetProvider.LoadAsync<T>(id);
            _container.Inject(character);
            return _charactersRepository.Register(character);
        }

        public async UniTask<T> CreateGameLoopCharacter<T>(string id)
            where T : Character, IGameLoopObject
        {
            T character = await _gameLoopCreator.Create<T>(id);
            return _charactersRepository.Register(character);
        }
    }
}