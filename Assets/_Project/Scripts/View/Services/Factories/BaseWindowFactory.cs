using _Project.Scripts.ViewModel.Base;
using UnityEngine.AddressableAssets;
using Zenject;

namespace _Project.Scripts.View.Services.Factories
{
    public class BaseWindowFactory<TWindow, TViewModel> : PlaceholderFactory<TWindow>
        where TViewModel : IViewModel
        where TWindow :  Window<TViewModel>
    {
        protected readonly TViewModel ViewModel;
        
        private readonly AssetReference _prefabReference;
        private readonly WindowCreator _windowCreator;
        
        protected BaseWindowFactory(
            TViewModel viewModel, 
            AssetReference prefabReference, 
            WindowCreator windowCreator)
        {
            ViewModel = viewModel;
            _prefabReference = prefabReference;
            _windowCreator = windowCreator;
        }

        public override TWindow Create() =>
            CreateFromCreator();
        
        protected TWindow CreateFromCreator() =>
            _windowCreator.Create<TWindow, TViewModel>(ViewModel, _prefabReference);
    }

    public abstract class BaseWindowFactory<TWindow, TViewModel, TParametor> : PlaceholderFactory<TParametor, TWindow>
        where TViewModel : IViewModel
        where TWindow :  Window<TViewModel>
    {
        protected readonly TViewModel ViewModel;
        
        private readonly AssetReference _prefabReference;
        private readonly WindowCreator _windowCreator;
        
        protected BaseWindowFactory(
            TViewModel viewModel, 
            AssetReference prefabReference, 
            WindowCreator windowCreator)
        {
            ViewModel = viewModel;
            _prefabReference = prefabReference;
            _windowCreator = windowCreator;
        }
        
        protected TWindow CreateFromCreator() =>
            _windowCreator.Create<TWindow, TViewModel>(ViewModel, _prefabReference);
    }

    public abstract class BaseWindowFactory<TWindow, TViewModel, TParametor1, TParametor2> : 
        PlaceholderFactory<TParametor1, TParametor2, TWindow>
        where TViewModel : IViewModel
        where TWindow :  Window<TViewModel>
    {
        protected readonly TViewModel ViewModel;
        
        private readonly AssetReference _prefabReference;
        private readonly WindowCreator _windowCreator;
        
        protected BaseWindowFactory(
            TViewModel viewModel, 
            AssetReference prefabReference, 
            WindowCreator windowCreator)
        {
            ViewModel = viewModel;
            _prefabReference = prefabReference;
            _windowCreator = windowCreator;
        }
        
        protected TWindow CreateFromCreator() =>
            _windowCreator.Create<TWindow, TViewModel>(ViewModel, _prefabReference);
    }
}