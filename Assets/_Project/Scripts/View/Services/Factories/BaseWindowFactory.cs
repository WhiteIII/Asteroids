using _Project.Scripts.Common;
using _Project.Scripts.ViewModel;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.Scripts.View.Services
{
    public class BaseWindowFactory<TWindow, TViewModel> : PlaceholderFactory<UniTask<TWindow>>
        where TViewModel : IViewModel
        where TWindow :  Window<TViewModel>
    {
        protected readonly TViewModel ViewModel;

        private readonly string _prefabId;
        private readonly UIRoot _uiRoot;
        private readonly DiContainer _container;
        private readonly LocalAssetProvider _localAssetProvider;
        private readonly WindowsRepository _windowsRepository;

        protected BaseWindowFactory(
            TViewModel viewModel,
            UIRoot uiRoot,
            WindowsRepository windowsRepository,
            string prefabId, 
            LocalAssetProvider localAssetProvider,
            DiContainer container)
        {
            ViewModel = viewModel;
            _uiRoot = uiRoot;
            _windowsRepository = windowsRepository;
            _prefabId = prefabId;
            _localAssetProvider = localAssetProvider;
            _container = container;
        }

        public override async UniTask<TWindow> Create()
        {
            TWindow window = await CreateWindow();
            window.Setup(ViewModel);
            return window;
        }
        
        protected async UniTask<TWindow> CreateWindow()
        {
            TWindow window = await _localAssetProvider.LoadAsync<TWindow>(_prefabId);
            _container.Inject(window);
            _uiRoot.AddWindow(window.transform);
            _windowsRepository.Register(window);
            return window;
        }
    }
}