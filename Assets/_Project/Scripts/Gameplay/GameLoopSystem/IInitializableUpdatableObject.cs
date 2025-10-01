namespace _Project.Scripts.Core.GameLoopSystem
{
    public interface IInitializableUpdatableObject : IGameLoopObject
    {
        IUpdatable[] GetAllUpdatableObjects();
    }
}