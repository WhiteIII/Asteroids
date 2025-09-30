namespace _Project.Scripts.Core.GameLoopSystem
{
    public interface IInitializableUpdatableObject
    {
        IUpdatable[] GetAllUpdatableObjects();
    }
}