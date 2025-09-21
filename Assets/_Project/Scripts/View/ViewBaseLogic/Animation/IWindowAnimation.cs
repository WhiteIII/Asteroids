using Cysharp.Threading.Tasks;

namespace _Project.Scripts.View.ViewBaseLogic.Animation
{
    public interface IWindowAnimation
    {
        UniTask PlayShowAnimation();
        UniTask PlayHideAnimation();
    }
}