using Cysharp.Threading.Tasks;

namespace _Project.Scripts.View.Animations.Base
{
    internal interface IWindowAnimation
    {
        UniTask PlayCloseAnimationAsync();
        UniTask PlayShowAnimationAsync();
    }
}