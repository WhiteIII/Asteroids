using System;
using _Project.Scripts.View;
using _Project.Scripts.View.Implementation;
using _Project.Scripts.View.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Bootstrap
{
    internal class MenuEntryPoint : IInitializable, IDisposable
    {
        private readonly IFactory<MenuWindow> _menuWindowFactory;
        private readonly WindowsRepository _windowsRepository;

        public MenuEntryPoint(
            IFactory<MenuWindow> menuWindowFactory,
            WindowsRepository windowsRepository)
        {
            _menuWindowFactory = menuWindowFactory;
            _windowsRepository = windowsRepository;
        }

        public async void Initialize()
        {
            MenuWindow menuWindow = _menuWindowFactory.Create();
            await menuWindow.Open();
        }

        public async void Dispose()
        {
            await _windowsRepository.TryCloseAndDestroyWindow<MenuWindow>();
        }
    }
}