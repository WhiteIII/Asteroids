using _Project.Scripts.Core.InputSystem;
using _Project.Scripts.Data;
using Zenject;

namespace _Project.Scripts.Bootstrap
{
    internal class EntryPoint : IInitializable
    {
        private readonly IInputHandler _inputHandler;
        private readonly ShipDefaultStats _shipDefaultStats;

        public EntryPoint(IInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }

        public async void Initialize()
        {
            
        }
    }
}
