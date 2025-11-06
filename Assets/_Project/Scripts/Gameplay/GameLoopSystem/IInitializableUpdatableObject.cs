namespace _Project.Scripts.Gameplay.GameLoopSystem
{
    public interface IInitializableUpdatableObject : IGameLoopObject
    {
        IGameLoopObject[] GetAllGameLoopObjects();
    }
}