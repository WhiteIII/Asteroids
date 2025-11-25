using _Project.Scripts.Gameplay.Ai.Base;
using _Project.Scripts.Gameplay.Ai.Implementation;
using _Project.Scripts.Gameplay.Characters;
using _Project.Scripts.Gameplay.Characters.Base;
using UnityEngine.AddressableAssets;
using Zenject;

namespace _Project.Scripts.Gameplay.Services.Factories
{
    public class UfoFactory : PlaceholderFactory<Ufo>
    {
        private readonly CharacterCreator _characterCreator;
        private readonly AiActorCreator _aiActorCreator;
        private readonly AssetReference _ufoAssetReference;

        public UfoFactory(
            AiActorCreator aiActorCreator,
            CharacterCreator characterCreator, 
            AssetReference ufoAssetReference)
        {
            _aiActorCreator = aiActorCreator;
            _characterCreator = characterCreator;
            _ufoAssetReference = ufoAssetReference;
        }

        public override Ufo Create()
        {
            Ufo ufo = _characterCreator.CreateNonGameLoopCharacter<Ufo>(_ufoAssetReference);
            _aiActorCreator.Create(
                new BaseRule(ufo.MoveToPlayer, () => ufo.PlayerIsClose == false && ufo.IsAlive),
                new BaseRule(ufo.Attack, () => ufo.PlayerIsClose && ufo.InCooldown == false && ufo.IsVisible && ufo.IsAlive),
                new BaseRule(ufo.StopMoving, () => ufo.PlayerIsClose && ufo.IsMovingStoped == false && ufo.IsAlive));

            return ufo;
        }        
    }
}