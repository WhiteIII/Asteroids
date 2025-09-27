using System.Collections.Generic;
using _Project.Scripts.Core.Enemies.Asteroids;

namespace _Project.Scripts.Core.Services.Repositories
{
    public class CharactersRepository : ICharacterRepository
    {
        private readonly List<Asteroid> _asteroidsList = new();
        private readonly List<Asteroid> _smallAsteroidsList = new();
        
        public Ship.Ship Ship { get; private set; }
        public int AsteroidsCount => _asteroidsList.Count + _smallAsteroidsList.Count;
        
        public void RegisterShip(Ship.Ship ship) => 
            Ship = ship;
        
        public void RegisterSmallAsteroid(Asteroid asteroid) =>
            _smallAsteroidsList.Add(asteroid);
        
        public void RegisterAsteroid(Asteroid asteroid) =>
            _asteroidsList.Add(asteroid);
        
        public void UnregisterAsteroid(Asteroid asteroid) =>
            _asteroidsList.Remove(asteroid);
        
        public void UnregisterSmallAsteroid(Asteroid asteroid) =>
            _smallAsteroidsList.Remove(asteroid);
        
        public void Clear()
        {
            Ship = null;
            _asteroidsList.Clear();
            _smallAsteroidsList.Clear();
        }
    }

    public interface ICharacterRepository
    {
        Ship.Ship Ship { get; }
        int AsteroidsCount { get; }
    }
}
