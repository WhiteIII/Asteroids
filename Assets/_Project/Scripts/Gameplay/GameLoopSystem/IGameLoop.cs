namespace _Project.Scripts.Gameplay.GameLoopSystem
{
    public interface IGameLoop
    {
        void AddUpdatable(IUpdatable item);
        void RemoveUpdatable(IUpdatable item);
        void AddPausedObject(IPausedCharacter item);
        void RemovePausedObject(IPausedCharacter item);
        void Resume();
        void Pause();
    }
}