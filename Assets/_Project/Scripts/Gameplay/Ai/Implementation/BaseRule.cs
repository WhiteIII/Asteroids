using System;
using _Project.Scripts.Gameplay.Ai.Base;

namespace _Project.Scripts.Gameplay.Ai.Implementation
{
    public class BaseRule : IRule
    {
        private readonly Action _onExecute;
        private readonly Func<bool> _canExecute;

        public bool CanExecute => _canExecute.Invoke();
        
        public BaseRule(Action onExecute, Func<bool> canExecute)
        {
            _onExecute = onExecute;
            _canExecute = canExecute;
        }
        
        public void Execute() => 
            _onExecute?.Invoke();
    }
}