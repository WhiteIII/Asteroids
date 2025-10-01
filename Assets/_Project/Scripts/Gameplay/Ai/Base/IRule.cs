namespace _Project.Scripts.Gameplay.Ai.Base
{
    public interface IRule
    {
        bool CanExecute { get; }
        void Execute();
    }
}