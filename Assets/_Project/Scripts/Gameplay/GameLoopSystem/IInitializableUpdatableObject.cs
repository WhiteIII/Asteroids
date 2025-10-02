namespace _Project.Scripts.Gameplay.GameLoopSystem
{
    public interface IInitializableUpdatableObject : IGameLoopObject
    {
        IUpdatable[] GetAllUpdatableObjects();
    }
}