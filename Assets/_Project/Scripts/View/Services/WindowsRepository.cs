using System.Collections.Generic;
using _Project.Scripts.Common;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.View.Services
{
    public class WindowsRepository
    {
        private readonly List<IWindow> _windows = new();

        private readonly LocalAssetProvider _localAssetProvider;

        public WindowsRepository(LocalAssetProvider localAssetProvider) =>
            _localAssetProvider = localAssetProvider;

        public T Get<T>()
            where T : class, IWindow
        {
            foreach (IWindow window in _windows)
            {
                if (window is T result) 
                    return result;
            }
            return null;
        }
        
        public void Register(IWindow window) => 
            _windows.Add(window);

        public void Destroy<T>()
            where T : MonoBehaviour, IWindow
        {
            foreach (IWindow window in _windows)
            {
                if (window is T behaviorWindow)
                {
                    _windows.Remove(window);
                    _localAssetProvider.Unload(behaviorWindow);
                    return;
                }
            }
        }
        
        public async UniTask TryCloseAndDestroyWindow<T>()
            where T : MonoBehaviour, IWindow
        {
            T window = Get<T>();
            if (!window)
                return;
            await window.Close();
            Destroy<T>();
        }
    }
}