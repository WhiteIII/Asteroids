using _Project.Scripts.View.Implementation;
using _Project.Scripts.ViewModel.Implementation;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.View.Services
{
    public class ShipStatsWindowFactory : BaseWindowFactory<ShipStatsWindow, ShipStatsViewModel>
    {
        protected ShipStatsWindowFactory(
            ShipStatsViewModel viewModel,
            AssetReference prefabReference,
            WindowCreator windowCreator) : base(viewModel, prefabReference, windowCreator)
        {
        }

        public override ShipStatsWindow Create()
        {
            ViewModel.SetShipObservables();
            ShipStatsWindow window = CreateFromCreator();
            window.Setup(ViewModel);
            return window;
        }
    }
}