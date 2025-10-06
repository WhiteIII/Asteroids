using _Project.Scripts.Gameplay.Enemies;
using _Project.Scripts.Gameplay.GameLoopSystem;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Services.Factories
{
    public class UfoFactory : PlaceholderFactory<Ufo>
    {
        private readonly IGameLoopCreator _creator;
        private readonly GameObject _ufoPrefab;
        
        public override Ufo Create()
        {
            Ufo ufo = _creator.CreateMonoBehaviourObject<Ufo>(_ufoPrefab);
            
            return base.Create();
        }        
    }
}