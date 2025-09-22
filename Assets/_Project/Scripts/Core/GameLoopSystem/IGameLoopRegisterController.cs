namespace _Project.Scripts.Core.GameLoopSystem
{
    public interface IGameLoopRegisterController
    {
        T Register<T>(T item) where T : IUpdatable;
        T Unregister<T>(T item) where T : IUpdatable;
    }
}