using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Characters.Base;
using _Project.Scripts.Gameplay.ShipBase;

namespace _Project.Scripts.Gameplay.Services.Repositories
{
    public class CharactersRepository : ICharacterRepository
    {
        private readonly List<ICharacter> _charactersList = new();
        
        public Ship Ship { get; private set; }

        private void RegisterShip(Ship ship) => 
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
            if (character is Ship ship && !Ship)
                RegisterShip(ship);
            else 
                _charactersList.Add(character);
            return character;
        }

        public void UnregisterShip()
        {
            if (Ship is IDisposable disposable)
                disposable.Dispose();
            Ship = null;
        } 
        
        public void ClearAllCharactersList()
        {
            foreach (ICharacter character in _charactersList)
            {
                if (character is IDisposable disposable)
                    disposable.Dispose();
            }
            _charactersList.Clear();
        }
    }

    public interface ICharacterRepository
    {
        Ship Ship { get; }
        int CharactersCount<T>() where T : ICharacter;
    }
}