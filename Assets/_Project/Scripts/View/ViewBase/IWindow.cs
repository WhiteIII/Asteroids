using System;
using _Project.Scripts.ViewModel;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.View
{
    public interface IWindow<in T> : IWindow
        where T : IViewModel
    {
        void Setup(T viewModel);
    }

    public interface IWindow
    {
        UniTask Open();
        UniTask Close();
    }
}
