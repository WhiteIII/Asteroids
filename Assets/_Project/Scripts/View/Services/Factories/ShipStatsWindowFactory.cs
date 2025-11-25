using _Project.Scripts.Common;
using _Project.Scripts.View.Implementation;
using _Project.Scripts.ViewModel.Implementation;
using UnityEngine.AddressableAssets;
using Zenject;

namespace _Project.Scripts.View.Services
{
    public class ShipStatsWindowFactory : BaseWindowFactory<ShipStatsWindow, ShipStatsViewModel>
    {
        protected ShipStatsWindowFactory(
            ShipStatsViewModel viewModel,
            UIRoot uiRoot,
            IInstantiator instantiator,
            WindowsRepository windowsRepository,
            LocalAssetProvider localAssetProvider,
            AssetReference prefabReference) : 
            base(viewModel, uiRoot, instantiator, windowsRepository, localAssetProvider, prefabReference)
        {
        }

        public override ShipStatsWindow Create()
        {
            ShipStatsWindow window = CreateWindow();
            ViewModel.SetShipObservables();
            window.Setup(ViewModel);
            return window;
        }
    }
}