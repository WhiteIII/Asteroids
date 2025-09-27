using System;
using _Project.Scripts.Core.Enemies.Asteroids;
using Zenject;

namespace _Project.Scripts.Core.Services.ObjectPools
{
    public class SmallAsteroidsPool : ItemsWithIdPool<Asteroid, string>
    {
        public SmallAsteroidsPool([Inject(Id = "SmallAsteroidsFactory")]IFactory<Asteroid> factory) : 
            base(factory, () => Guid.NewGuid().ToString())
        {
        }
    }
}