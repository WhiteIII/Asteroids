namespace _Project.Scripts.Core.Services.Repositories
{
    public class CharactersRepository : ICharacterRepository
    {
        public Ship.Ship Ship { get; private set; }
        
        public void RegisterShip(Ship.Ship ship) => 
            Ship = ship;

        public void Clear()
        {
            Ship = null;
        }
    }

    public interface ICharacterRepository
    {
        Ship.Ship Ship { get; }
    }
}
