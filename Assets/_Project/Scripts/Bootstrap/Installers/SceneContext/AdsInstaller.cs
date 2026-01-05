using _Project.Scripts.Bootstrap.EntryPoints;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Bootstrap.Installers.SceneContext
{
    internal class AdsInstaller : MonoInstaller
    {
        [SerializeField] string _androidGameId;
        [SerializeField] string _iOSGameId;
        [SerializeField] bool _testMode = true;
        
        public override void InstallBindings() =>
            Container
                .BindInterfacesTo<UnityAdsInitializer>()
                .AsSingle()
                .WithArguments(new UnityAdsInitializerData
                {
                    AndroidGameId = _androidGameId, 
                    IOSGameId = _iOSGameId,
                    TestMode = _testMode
                });
    }
}