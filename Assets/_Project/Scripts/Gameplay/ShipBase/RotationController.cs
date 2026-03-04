using _Project.Scripts.Data.Implementation;
using _Project.Scripts.Data.Services.Repositories.Base;
using _Project.Scripts.Gameplay.Characters.Base;
using _Project.Scripts.Gameplay.GameLoopSystem;
using _Project.Scripts.Gameplay.InputSystem;
using UnityEngine;
using Zenject;
using static UnityEngine.Time;

namespace _Project.Scripts.Gameplay.ShipBase
{
    public class RotationController : MonoBehaviour, IUpdatable
    {
        private Character _character;
        private IReadOnlyInputHandler _readOnlyInputHandler;
        private float _rotationSpeed;

        [Inject]
        private void Construct(IReadOnlyInputHandler readOnlyInputHandler, IDataRepository dataRepository)
        {
            _readOnlyInputHandler = readOnlyInputHandler;
            _rotationSpeed = dataRepository.GetData<ShipStatsConfig>().RotationSpeed;
        }

        private void Awake() => 
            _character = GetComponent<Character>();

        public void GameLoopUpdate()
        {
            if (_character.IsVisible == false)
                return;
            
            transform.Rotate(
                0f,
                0f,
                -_readOnlyInputHandler.Horizontal.CurrentValue * _rotationSpeed * deltaTime);
        }

        public void SetRotation(Quaternion rotation) => 
            transform.rotation = rotation; 
    }
}