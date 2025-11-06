namespace _Project.Scripts.Gameplay.GameLoopSystem
{
    public class GameLoopRegisterController : IGameLoopRegisterController
    {
        private readonly IGameLoop _gameLoop;

        public GameLoopRegisterController(IGameLoop gameLoop) => 
            _gameLoop = gameLoop;

        public T Register<T>(T item) 
            where T : IUpdatable
        {
            _gameLoop.AddUpdatable(item);
            return item;
        }

        public T Unregister<T>(T item) 
            where T : IUpdatable
        {
            _gameLoop.RemoveUpdatable(item);
            return item;
        }

        public T RegisterPausableObject<T>(T item)
            where T : IPausedCharacter
        {
            _gameLoop.AddPausedObject(item);
            return item;
        }
        
        public T UnregisterPausableObject<T>(T item)
            where T : IPausedCharacter
        {
            _gameLoop.RemovePausedObject(item);
            return item;
        }

        public T RegisterInitializableObject<T>(T item) 
            where T : IInitializableUpdatableObject
        {
            foreach (IGameLoopObject gameLoopObject in item.GetAllGameLoopObjects())
            {
                if (gameLoopObject is IUpdatable updatableObject) 
                    Register(updatableObject);
                else if (gameLoopObject is IPausedCharacter pausedObject)
                    RegisterPausableObject(pausedObject);
            }
            return item;
        }
        
        public T UnregisterInitializableObject<T>(T item)
            where T : IInitializableUpdatableObject
        {
            foreach (IGameLoopObject gameLoopObject in item.GetAllGameLoopObjects())
            {
                if (gameLoopObject is IUpdatable updatableObject) 
                    Unregister(updatableObject);
                else if (gameLoopObject is IPausedCharacter pausedObject)
                    UnregisterPausableObject(pausedObject);
            }
            return item;
        }
    }
}