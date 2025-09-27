using System;
using _Project.Scripts.Core.Enemies.Asteroids;
using Zenject;

namespace _Project.Scripts.Core.Services.ObjectPools
{
    public class AsteroidsPool : ItemsWithIdPool<Asteroid, string>
    {
        public AsteroidsPool([Inject(Id = "AsteroidsFactory")]IFactory<Asteroid> factory) : base(factory, () => Guid.NewGuid().ToString())
        {
        }
    }
}