using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Ai.Base;

namespace _Project.Scripts.Gameplay.Services.Repositories
{
    public class AiActorsRepository
    {
        private readonly List<IAiActor> _actors = new();
        
        public T Register<T>(T actor) where T : IAiActor
        {
            _actors.Add(actor);
            return actor;
        }
        
        public void Unregister<T>(T actor) where T : IAiActor => 
            _actors.Remove(actor);
        
        public void Clear()
        {
            foreach (IAiActor actor in _actors)
            {
                if (actor is IDisposable disposable)
                    disposable.Dispose();
            }
            
            _actors.Clear();
        }
    }
}