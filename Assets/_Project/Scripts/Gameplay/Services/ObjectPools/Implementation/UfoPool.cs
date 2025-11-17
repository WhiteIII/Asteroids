using System;
using _Project.Scripts.Gameplay.Characters;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Services.ObjectPools
{
    public class UfoPool : ItemsWithIdAndParameterPool<Ufo, string, Vector2>
    {
        public UfoPool(
            IFactory<UniTask<Ufo>> ufoFactory) : 
            base(
                ufoFactory.Create, 
                () => Guid.NewGuid().ToString(),
                (ufo, position) => ufo.SetPosition(position),
                true)
        {
            
        }
    }
}