namespace _Project.Scripts.Core.GameLoopSystem
{
    public class GameLoopRegisterController : IGameLoopRegisterController
    {
        private readonly IGameLoop _gameLoop;

        public GameLoopRegisterController(IGameLoop gameLoop) => 
            _gameLoop = gameLoop;

        public T Register<T>(T item) 
            where T : IUpdatable
        {
            _gameLoop.Add(item);
            return item;
        }

        public T Unregister<T>(T item) 
            where T : IUpdatable
        {
            _gameLoop.Remove(item);
            return item;
        }

        public T RegisterInitializableObject<T>(T item) 
            where T : IInitializableUpdatableObject
        {
            foreach (IUpdatable updatableObject in item.GetAllUpdatableObjects())
                _gameLoop.Add(updatableObject);
            return item;
        }
        
        public T UnregisterInitializableObject<T>(T item)
            where T : IInitializableUpdatableObject
        {
            foreach (IUpdatable updatableObject in item.GetAllUpdatableObjects())
                _gameLoop.Remove(updatableObject);
            return item;
        }
    }
}