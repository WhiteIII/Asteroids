using System;
using _Project.Scripts.View.ViewBaseLogic;

namespace _Project.Scripts.View.Implementation
{
    public class MainMenuViewModel : IViewModel
    {
        private Action GoToMenuAction;

        public void Initialize(Action goToMenuAction) =>
            GoToMenuAction = goToMenuAction;
        
        public void GoToMenu() => 
            GoToMenuAction?.Invoke();
    }
}