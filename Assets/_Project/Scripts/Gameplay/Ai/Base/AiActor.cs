namespace _Project.Scripts.Gameplay.Ai.Base
{
    public class AiActor : IAiActor
    {
        private readonly IRule[] _rules;

        public AiActor(params IRule[] rules) => 
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
