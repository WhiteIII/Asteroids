using _Project.Scripts.View.Implementation;
using _Project.Scripts.ViewModel.Implementation;
using Zenject;

namespace _Project.Scripts.View.Services
{
    public class ShipStatsWindowFactory : BaseWindowFactory<ShipStatsWindow, ShipStatsViewModel>
    {
        protected ShipStatsWindowFactory(
            ShipStatsViewModel viewModel, 
            ShipStatsWindow prefab,
            UIRoot uiRoot,
            IInstantiator instantiator,
            WindowsRepository windowsRepository) : base(viewModel, prefab, uiRoot, instantiator, windowsRepository)
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