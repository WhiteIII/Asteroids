using _Project.Scripts.Gameplay.Characters.Base;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.Scripts.Gameplay.Services.Factories
{
    public class ShipFactory : PlaceholderFactory<UniTask<Ship.Ship>>
    {
        private const string ID = "Ship";

        private readonly CharacterCreator _characterCreator;

        public ShipFactory(CharacterCreator characterCreator) => 
            _characterCreator = characterCreator;

        public override UniTask<Ship.Ship> Create() => 
            _characterCreator.CreateGameLoopCharacter<Ship.Ship>(ID);
    }
}