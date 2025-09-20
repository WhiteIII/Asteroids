using Cysharp.Threading.Tasks;

namespace _Project.Scripts.View.ViewBaseLogic
{
    public interface IWindow<in T>
    {
        void Setup(T viewModel);
        UniTask Open();
        UniTask Close();   
    }
}
