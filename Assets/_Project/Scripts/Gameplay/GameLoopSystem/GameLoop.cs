using System.Collections.Generic;
using Zenject;

namespace _Project.Scripts.Gameplay.GameLoopSystem
{
    public class GameLoop : IGameLoop, ITickable
    {
        private readonly List<IUpdatable> _updatables = new();
        private readonly Queue<IUpdatable> _addedUpdateablesQueue = new();
        private readonly Queue<IUpdatable> _removedUpdateablesQueue = new();
        
        private bool _isPaused;
        
        public void Tick()
        {
            if (_isPaused)
                return;
            
            foreach (IUpdatable updatable in _updatables)
                updatable.GameLoopUpdate();

            while (_addedUpdateablesQueue.Count > 0)
                _updatables.Add(_addedUpdateablesQueue.Dequeue());
            while (_removedUpdateablesQueue.Count > 0)
                _updatables.Remove(_removedUpdateablesQueue.Dequeue());
        }
        
        public void Add(IUpdatable updatable) =>
            _addedUpdateablesQueue.Enqueue(updatable);
        
        public void Remove(IUpdatable updatable) => 
            _removedUpdateablesQueue.Enqueue(updatable);
        
        public void Pause() => 
            _isPaused = true;

        public void Resume() =>
            _isPaused = false;
    }
}