using _Project.Scripts.Gameplay.Ai.Base;
using _Project.Scripts.Gameplay.Ai.Implementation;
using _Project.Scripts.Gameplay.Characters;
using _Project.Scripts.Gameplay.Characters.Base;
using Zenject;

namespace _Project.Scripts.Gameplay.Services.Factories
{
    public class UfoFactory : PlaceholderFactory<Ufo>
    {
        private readonly CharacterCreator _characterCreator;
        private readonly AiActorCreator _aiActorCreator;
        private readonly Ufo _ufoPrefab;

        public UfoFactory(
            AiActorCreator aiActorCreator,
            Ufo ufoPrefab,
            CharacterCreator characterCreator)
        {
            _aiActorCreator = aiActorCreator;
            _ufoPrefab = ufoPrefab;
            _characterCreator = characterCreator;
        }

        public override Ufo Create()
        {
            Ufo ufo = _characterCreator.CreateNonGameLoopCharacter(_ufoPrefab);
            _aiActorCreator.Create(
                new BaseRule(ufo.MoveToPlayer, () => ufo.PlayerIsClose == false),
                new BaseRule(ufo.Attack, () => ufo.PlayerIsClose && ufo.InCooldown == false),
                new BaseRule(ufo.StopMoving, () => ufo.PlayerIsClose && ufo.IsMovingStoped == false));

            return ufo;
        }        
    }
}