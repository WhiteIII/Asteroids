namespace _Project.Scripts.Gameplay.GameLoopSystem
{
    public interface IGameLoop
    {
        void Add(IUpdatable item);
        void Remove(IUpdatable item);
        void Resume();
        void Pause();
    }
}