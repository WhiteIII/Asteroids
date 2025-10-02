using System;
using _Project.Scripts.Gameplay.Ai.Base;

namespace _Project.Scripts.Gameplay.Ai.Implementation
{
    public class BaseRule : IRule
    {
        private readonly Action _onAttack;
        private readonly Func<bool> _canExecute;

        public bool CanExecute => _canExecute();
        
        public BaseRule(Action onAttack, Func<bool> canExecute)
        {
            _onAttack = onAttack;
            _canExecute = canExecute;
        }
        
        public void Execute() => 
            _onAttack?.Invoke();
    }
}