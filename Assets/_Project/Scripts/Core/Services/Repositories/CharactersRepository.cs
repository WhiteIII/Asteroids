using System.Collections.Generic;
using _Project.Scripts.Core.Enemies.Asteroids;
using _Project.Scripts.Core.GameLoopSystem;
using _Project.Scripts.Core.ShootingSystem;

namespace _Project.Scripts.Core.Services.Repositories
{
    public class CharactersRepository : ICharacterRepository
    {
        private readonly IGameLoopRegisterController _gameLoopRegisterController;
        private readonly List<Bullet> _bullets = new();
        private readonly List<Asteroid> _asteroidsList = new();
        private readonly List<Asteroid> _smallAsteroidsList = new();
        
        public Ship.Ship Ship { get; private set; }
        public int AsteroidsCount => _asteroidsList.Count + _smallAsteroidsList.Count;
        public int BulletsCount => _bullets.Count;

        public CharactersRepository(IGameLoopRegisterController gameLoopRegisterController) => 
            _gameLoopRegisterController = gameLoopRegisterController;

        public void RegisterShip(Ship.Ship ship)
        {
            Ship = ship;
            _gameLoopRegisterController.RegisterInitializableObject(Ship);
        }
     
        public void UnregisterShip()
        {
            _gameLoopRegisterController.UnregisterInitializableObject(Ship);
            Ship = null;
        }

        public void RegisterSmallAsteroid(Asteroid asteroid)
        {
            _smallAsteroidsList.Add(asteroid);
            _gameLoopRegisterController.RegisterInitializableObject(asteroid);
        }

        public void RegisterAsteroid(Asteroid asteroid)
        {
            _asteroidsList.Add(asteroid);
            _gameLoopRegisterController.RegisterInitializableObject(asteroid);
        }

        public void RegisterBullet(Bullet bullet)
        {
            _bullets.Add(bullet);
            _gameLoopRegisterController.RegisterInitializableObject(bullet);
        }
        
        public void Clear()
        {
            UnregisterShip();

            foreach (Asteroid asteroid in _asteroidsList)
                _gameLoopRegisterController.UnregisterInitializableObject(asteroid);
            foreach (Asteroid asteroid in _smallAsteroidsList)
                _gameLoopRegisterController.UnregisterInitializableObject(asteroid);
            foreach (Bullet bullet in _bullets)
                _gameLoopRegisterController.UnregisterInitializableObject(bullet);
            
            _asteroidsList.Clear();
            _smallAsteroidsList.Clear();
            _bullets.Clear();
        }
    }

    public interface ICharacterRepository
    {
        Ship.Ship Ship { get; }
        int AsteroidsCount { get; }
    }
}