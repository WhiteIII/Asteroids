using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.Gameplay.Services.Components
{
    [RequireComponent(typeof(NavMeshAgent))]
    internal class AiAgentMovement : MonoBehaviour, IAiAgentMovement
    {
        private readonly float _updatePath = 0.1f;
        
        private NavMeshAgent _agent;
        private float _movementSpeed;
        private bool _inCoolDown;

        public void Initialize(float movementSpeed)
        {
            _movementSpeed = movementSpeed;
            _agent.speed = _movementSpeed;
        }
                
        private void Awake() => 
            _agent = GetComponent<NavMeshAgent>();

        public void SetPosition(Vector2 position) => 
            transform.position = position;

        public async void MoveTo(Vector2 shipPosition)
        {
            Enable();
            if (_inCoolDown)
                return;
            
            _agent.SetDestination(shipPosition);
            _inCoolDown = true;
            await UniTask.WaitForSeconds(_updatePath);
            _inCoolDown = false;
        }

        public void StopMoving() => 
            Disable();

        private void Disable() => 
            _agent.speed = 0f;
        
        private void Enable() => 
            _agent.speed = _movementSpeed;
    }
}