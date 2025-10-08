using UnityEngine;

namespace _Project.Scripts.Gameplay.Services.Components
{
    internal interface IAiAgentMovement
    {
        void Initialize(float movementSpeed);
        void MoveTo(Vector2 shipPosition);
        void SetPosition(Vector2 position);
    }
}