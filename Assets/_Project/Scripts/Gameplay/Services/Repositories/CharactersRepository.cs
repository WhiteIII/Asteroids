using System;
using System.Collections.Generic;
using _Project.Scripts.Common;
using _Project.Scripts.Gameplay.Characters.Base;

namespace _Project.Scripts.Gameplay.Services.Repositories
{
    public class CharactersRepository : ICharacterRepository
    {
        private readonly List<ICharacter> _charactersList = new();
        
        private readonly LocalAssetProvider _localAssetProvider;

        public CharactersRepository(LocalAssetProvider localAssetProvider) =>
            _localAssetProvider = localAssetProvider;

        public Ship.Ship Ship { get; private set; }

        private void RegisterShip(Ship.Ship ship) => 
            Ship = ship;

        public int CharactersCount<T>()
            where T : ICharacter
        {
            int count = 0;
            foreach (ICharacter character in _charactersList)
            {
                if (character.GetType() == typeof(T))
                    count++;
            }
            return count;
        }
        
        public T Register<T>(T character) 
            where T : ICharacter
        {
            if (character is Ship.Ship ship && !Ship)
                RegisterShip(ship);
            else 
                _charactersList.Add(character);
            return character;
        }
        
        public void DestroyShip()
        {
            if (Ship is IDisposable disposable)
                disposable.Dispose();
            _localAssetProvider.Unload(Ship);
            Ship = null;
        }

        public void DestroyAllCharacters()
        {
            foreach (ICharacter character in _charactersList)
            {
                if (character is IDisposable disposable)
                    disposable.Dispose();
                if (character is Character characterMonoBehaviour)
                    _localAssetProvider.Unload(characterMonoBehaviour);
            }
            _charactersList.Clear();
        }
    }

    public interface ICharacterRepository
    {
        Ship.Ship Ship { get; }
        int CharactersCount<T>() where T : ICharacter;
    }
}