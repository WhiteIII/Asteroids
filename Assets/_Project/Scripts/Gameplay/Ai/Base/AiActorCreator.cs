using _Project.Scripts.Gameplay.GameLoopSystem;
using _Project.Scripts.Gameplay.Services.Repositories;

namespace _Project.Scripts.Gameplay.Ai.Base
{
    public class AiActorCreator
    {
        private readonly AiActorsRepository _repository;
        private readonly IGameLoopCreator _creator;

        public AiActorCreator(AiActorsRepository repository, IGameLoopCreator creator)
        {
            _repository = repository;
            _creator = creator;
        }

        public void Create(params IRule[] rules) => 
            _repository.Register(_creator.RegisterObject(new AiActor(rules)));
    }
}