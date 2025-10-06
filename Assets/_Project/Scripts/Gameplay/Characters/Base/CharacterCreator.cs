using _Project.Scripts.Gameplay.GameLoopSystem;
using _Project.Scripts.Gameplay.Services.Repositories;
using Zenject;

namespace _Project.Scripts.Gameplay.Characters.Base
{
    public class CharacterCreator
    {
        private readonly IInstantiator _instantiator;
        private readonly IGameLoopCreator _gameLoopCreator;
        private readonly CharactersRepository _charactersRepository;

        public CharacterCreator(
            IInstantiator instantiator, 
            IGameLoopCreator gameLoopCreator,
            CharactersRepository charactersRepository)
        {
            _instantiator = instantiator;
            _gameLoopCreator = gameLoopCreator;
            _charactersRepository = charactersRepository;
        }

        public T CreateNonGameLoopCharacter<T>(T prefab)
            where T : Character
        {
            return _charactersRepository.Register(_instantiator.InstantiatePrefab(prefab).GetComponent<T>());
        }

        public T CreateGameLoopCharacter<T>(T prefab)
            where T : Character, IGameLoopObject
        {
            return _charactersRepository.Register(_gameLoopCreator.Create(prefab));
        }
    }
}