using System.Collections.Generic;
using Zenject;

namespace _Project.Scripts.Core.GameLoopSystem
{
    public class GameLoop : IGameLoop, ITickable
    {
        private readonly List<IUpdatable> _updateables = new();
        
        private bool _isPaused;
        
        public void Tick()
        {
            if (_isPaused)
                return;
            
            foreach (IUpdatable updatable in _updateables)
                updatable.Update();       
        }
        
        public void Add(IUpdatable updatable) =>
            _updateables.Add(updatable);
        
        public void Remove(IUpdatable updatable) => 
            _updateables.Remove(updatable);
        
        public void Pause() => 
            _isPaused = true;

        public void Resume() =>
            _isPaused = false;
    }
}
