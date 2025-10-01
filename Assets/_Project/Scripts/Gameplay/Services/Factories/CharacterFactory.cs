using _Project.Scripts.Core.Enemies.Base;
using _Project.Scripts.Core.GameLoopSystem;
using _Project.Scripts.Core.Services.Repositories;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core.Services.Factories
{
    public class CharacterFactory<T> : PlaceholderFactory<T>
        where T : MonoBehaviour, ICharacter, IGameLoopObject
    {
        private readonly GameObject _prefab;
        private readonly CharactersRepository _repository;
        private readonly IGameLoopCreator _creator;

        public CharacterFactory(
            GameObject prefab,
            CharactersRepository repository,
            IGameLoopCreator creator)
        {
            _prefab = prefab;
            _repository = repository;
            _creator = creator;
        }

        public override T Create() =>
            _repository.Register(_creator.Create<T>(_prefab));
    }
}