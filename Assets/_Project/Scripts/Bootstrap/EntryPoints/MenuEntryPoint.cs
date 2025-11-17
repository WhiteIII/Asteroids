using System;
using _Project.Scripts.View.Implementation;
using _Project.Scripts.View.Services;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.Scripts.Bootstrap.EntryPoints
{
    internal class MenuEntryPoint : IInitializable, IDisposable
    {
        private readonly IFactory<UniTask<MenuWindow>> _menuWindowFactory;
        private readonly WindowsRepository _windowsRepository;

        public MenuEntryPoint(
            IFactory<UniTask<MenuWindow>> menuWindowFactory,
            WindowsRepository windowsRepository)
        {
            _menuWindowFactory = menuWindowFactory;
            _windowsRepository = windowsRepository;
        }

        public async void Initialize()
        {
            MenuWindow menuWindow = await _menuWindowFactory.Create();
            await menuWindow.Open();
        }

        public async void Dispose() =>
            await _windowsRepository.TryCloseAndDestroyWindow<MenuWindow>();
    }
}