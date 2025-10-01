using _Project.Scripts.Core.GameLoopSystem;

namespace _Project.Scripts.Gameplay.Ai.Base
{
    public class AiActor : IUpdatable
    {
        private readonly IRule[] _rules;

        public AiActor(IRule[] rules) => 
            _rules = rules;

        public void GameLoopUpdate()
        {
            foreach (IRule rule in _rules)
            {
                if (rule.CanExecute)
                {
                    rule.Execute();
                    return;
                }
            }
        }
    }
}
