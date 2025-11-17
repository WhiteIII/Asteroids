using _Project.Scripts.Common;
using _Project.Scripts.View.Implementation;
using _Project.Scripts.ViewModel.Implementation;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.Scripts.View.Services
{
    public class ShipStatsWindowFactory : BaseWindowFactory<ShipStatsWindow, ShipStatsViewModel>
    {
        protected ShipStatsWindowFactory(
            ShipStatsViewModel viewModel,
            UIRoot uiRoot,
            WindowsRepository windowsRepository,
            string prefabId,
            LocalAssetProvider localAssetProvider,
            DiContainer container) : base(viewModel, uiRoot, windowsRepository, prefabId, localAssetProvider, container)
        {
        }

        public override async UniTask<ShipStatsWindow> Create()
        {
            ShipStatsWindow window = await CreateWindow();
            ViewModel.SetShipObservables();
            window.Setup(ViewModel);
            return window;
        }
    }
}