using System;
using _Project.Scripts.ViewModel;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.View
{
    public interface IWindow<in T>
        where T : IViewModel
    {
        void Setup(T viewModel);
        UniTask Open();
        UniTask Close();
    }
}
