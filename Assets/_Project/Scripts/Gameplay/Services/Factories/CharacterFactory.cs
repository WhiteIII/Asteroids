using _Project.Scripts.Gameplay.Enemies.Base;
using _Project.Scripts.Gameplay.GameLoopSystem;
using _Project.Scripts.Gameplay.Services.Repositories;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Services.Factories
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
            _repository.Register(_creator.CreateMonoBehaviourObject<T>(_prefab));
    }
}