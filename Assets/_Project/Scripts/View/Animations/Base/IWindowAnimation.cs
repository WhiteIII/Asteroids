using System.Threading;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.View.Animations.Base
{
    internal interface IWindowAnimation
    {
        UniTask PlayCloseAnimationAsync(CancellationToken cancellationToken = default);
        UniTask PlayShowAnimationAsync(CancellationToken cancellationToken = default);
    }
}