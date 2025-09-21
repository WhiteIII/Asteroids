using Cysharp.Threading.Tasks;

namespace _Project.Scripts.View.ViewBaseLogic
{
    public interface IWindow<in T>
        where T : IViewModel
    {
        void Setup(T viewModel);
        UniTask Open();
        UniTask Close();   
    }
}
