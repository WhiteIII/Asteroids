using R3;

namespace _Project.Scripts.Gameplay.InputSystem
{
    public interface IReadOnlyInputHandler
    {
        ReadOnlyReactiveProperty<float> Vertical { get; }
        ReadOnlyReactiveProperty<float> Horizontal { get; }
        Observable<Unit> OnSpacePressed { get; } 
        Observable<Unit> OnEKeyPressed { get; }
    }
}