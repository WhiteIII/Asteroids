using _Project.Scripts.Core.Services;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Bootstrap.Installers
{
    internal class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private Transform _bulletsParent;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<BulletPool>()
                .AsSingle()
                .WithArguments(_bulletsParent);
        }
    }
}