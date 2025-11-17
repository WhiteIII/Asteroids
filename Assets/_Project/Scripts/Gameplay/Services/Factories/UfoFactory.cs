using _Project.Scripts.Gameplay.Ai.Base;
using _Project.Scripts.Gameplay.Ai.Implementation;
using _Project.Scripts.Gameplay.Characters;
using _Project.Scripts.Gameplay.Characters.Base;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.Scripts.Gameplay.Services.Factories
{
    public class UfoFactory : PlaceholderFactory<UniTask<Ufo>>
    {
        private const string ID = "Ufo";
        
        private readonly CharacterCreator _characterCreator;
        private readonly AiActorCreator _aiActorCreator;

        public UfoFactory(
            AiActorCreator aiActorCreator,
            CharacterCreator characterCreator)
        {
            _aiActorCreator = aiActorCreator;
            _characterCreator = characterCreator;
        }

        public override async UniTask<Ufo> Create()
        {
            Ufo ufo = await _characterCreator.CreateNonGameLoopCharacter<Ufo>(ID);
            _aiActorCreator.Create(
                new BaseRule(ufo.MoveToPlayer, () => ufo.PlayerIsClose == false && ufo.IsAlive),
                new BaseRule(ufo.Attack, () => ufo.PlayerIsClose && ufo.InCooldown == false && ufo.IsVisible && ufo.IsAlive),
                new BaseRule(ufo.StopMoving, () => ufo.PlayerIsClose && ufo.IsMovingStoped == false && ufo.IsAlive));

            return ufo;
        }        
    }
}