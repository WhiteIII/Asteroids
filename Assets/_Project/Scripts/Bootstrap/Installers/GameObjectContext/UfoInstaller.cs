using System;
using _Project.Scripts.Gameplay.Ai.Base;
using _Project.Scripts.Gameplay.Ai.Implementation;
using _Project.Scripts.Gameplay.Enemies;
using Zenject;

namespace _Project.Scripts.Bootstrap.Installers.GameObjectContext
{
    public class UfoInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Ufo ufo = GetComponent<Ufo>();
            Container.Bind<IRule>()
                .To<BaseRule>()
                .AsSingle()
                .WithArguments<Action, Func<bool>>(() => ufo.Attack(), () => ufo.PlayerIsClose && ufo.InCooldown == false);
            Container
                .Bind<IRule>()
                .To<BaseRule>()
                .AsSingle()
                .WithArguments<Action, Func<bool>>(() => ufo.MoveToPlayer(), () => ufo.PlayerIsClose == false);
            Container
                .Bind<IRule>()
                .To<BaseRule>()
                .AsSingle()
                .WithArguments<Action, Func<bool>>(() => ufo.StopMoving(), () => ufo.PlayerIsClose && ufo.IsMovingStoped == false);
            Container.Bind<AiActor>().AsSingle();           
        }
    }   
}
