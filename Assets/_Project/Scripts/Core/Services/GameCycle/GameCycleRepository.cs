using System.Collections.Generic;
using Zenject;

namespace _Project.Scripts.Core.Services.GameCycle
{
    public class GameCycleRepository : 
        ITickable, 
        IInitializable, 
        IGameCycleRepository,
        IGameCyclePauseResumeController
    {
        private readonly List<IUpdatable> _updatablesList = new();
        
        private bool _isPaused;
        
        public void Initialize() => 
            Resume();

        public void Tick()
        {
            if (_isPaused)
                return;

            foreach (IUpdatable updatable in _updatablesList)
                updatable.Update();
        }

        public void Register(IUpdatable item) => 
            _updatablesList.Add(item);

        public void Unregister(IUpdatable item) =>
            _updatablesList.Remove(item);

        public void Pause() =>
            _isPaused = true;

        public void Resume() => 
            _isPaused = false;
    }

    public interface IGameCycleRepository
    {
        void Register(IUpdatable item);
        void Unregister(IUpdatable item);
        void Pause();
        void Resume();
    }

    public interface IGameCyclePauseResumeController
    {
        void Pause();
        void Resume();
    }
}
