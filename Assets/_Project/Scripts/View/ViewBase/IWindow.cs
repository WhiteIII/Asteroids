using _Project.Scripts.ViewModel;
using _Project.Scripts.ViewModel.Base;
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
        UniTask OpenAsync();
        UniTask CloseAsync();
    }
}
