using System;
using _Project.Scripts.Gameplay.Characters.Implementation;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Services.ObjectPools
{
    public class UfoPool : ItemsWithIdAndParameterPool<Ufo, string, Vector2>
    {
        public UfoPool(
            IFactory<Ufo> ufoFactory) : 
            base(
                () => ufoFactory.Create(), 
                () => Guid.NewGuid().ToString(),
                (ufo, position) =>
                {
                    ufo.Revive();
                    ufo.SetPosition(position);
                },
                true)
        {
            
        }
    }
}