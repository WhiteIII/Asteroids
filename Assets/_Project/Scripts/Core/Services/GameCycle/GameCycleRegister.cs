namespace _Project.Scripts.Core.Services.GameCycle
{
    public class GameCycleRegisterController : IGameCycleRegisterController
    {
        private readonly IGameCycleRepository _gameCycleRepository;

        public GameCycleRegisterController(IGameCycleRepository gameCycleRepository) =>
            _gameCycleRepository = gameCycleRepository;

        public T Register<T>(T item) 
            where T : IUpdatable
        {
            _gameCycleRepository.Register(item);
            return item;
        }

        public T Unregister<T>(T item)
            where T : IUpdatable
        {
            _gameCycleRepository.Unregister(item);
            return item;
        }
    }

    public interface IGameCycleRegisterController
    {
        T Register<T>(T item) where T : IUpdatable;

        T Unregister<T>(T item) where T : IUpdatable;
    }
}