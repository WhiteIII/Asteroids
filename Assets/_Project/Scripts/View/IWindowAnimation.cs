using Cysharp.Threading.Tasks;

namespace _Project.Scripts.View
{
    internal interface IWindowAnimation
    {
        UniTask PlayCloseAnimationAsync();
        UniTask PlayShowAnimationAsync();
    }
}