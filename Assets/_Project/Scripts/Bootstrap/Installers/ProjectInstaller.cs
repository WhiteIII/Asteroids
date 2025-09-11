using _Project.Scripts.Core.InputSystem;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        //Container.BindInterfacesTo<InputHandler>().AsSingle();
    }
}