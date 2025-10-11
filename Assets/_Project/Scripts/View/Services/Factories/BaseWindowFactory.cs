using _Project.Scripts.ViewModel;
using Zenject;

namespace _Project.Scripts.View.Services
{
    public class BaseWindowFactory<TWindow, TViewModel> : PlaceholderFactory<TWindow>
        where TViewModel : IViewModel
        where TWindow :  Window<TViewModel>
    {
        protected readonly TViewModel ViewModel;
        
        private readonly TWindow _prefab;
        private readonly UIRoot _uiRoot;
        private readonly IInstantiator _instantiator;
        private readonly WindowsRepository _windowsRepository;

        protected BaseWindowFactory(
            TViewModel viewModel,
            TWindow prefab,
            UIRoot uiRoot,
            IInstantiator instantiator,
            WindowsRepository windowsRepository)
        {
            ViewModel = viewModel;
            _prefab = prefab;
            _uiRoot = uiRoot;
            _instantiator = instantiator;
            _windowsRepository = windowsRepository;
        }

        public override TWindow Create()
        {
            TWindow window = CreateWindow();
            window.Setup(ViewModel);
            return window;
        }
        
        protected TWindow CreateWindow()
        {
            TWindow window = _instantiator
                .InstantiatePrefab(_prefab)
                .GetComponent<TWindow>();
            _uiRoot.AddWindow(window.transform);
            _windowsRepository.Register(window);
            return window;
        }
    }
}