using _Project.Scripts.Bootstrap;
using _Project.Scripts.Core.InputSystem;
using _Project.Scripts.Core.Services.GameCycle;
using _Project.Scripts.Core.Stats;
using _Project.Scripts.Data;
using UnityEngine;
using Zenject;

internal class GameInstaller : MonoInstaller
{
    [Header("Prefabs")]
    [SerializeField] private GameObject _shipPrefab;
    
    [Header("Data")]
    [SerializeField] private IShipDefaultStats _shipDefaultStats;
    
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<ShipStats>().AsSingle();
        Container.BindInterfacesTo<InputHandler>().AsSingle();
        Container.BindInterfacesTo<GameCycleRepository>().AsSingle();
        Container.BindInterfacesTo<EntryPoint>().AsSingle();
    }
}