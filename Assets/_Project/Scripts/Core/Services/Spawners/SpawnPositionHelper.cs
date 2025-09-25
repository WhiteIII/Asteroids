using UnityEngine;

namespace _Project.Scripts.Core.Services.Spawners
{
    public class SpawnPositionHelper
    {
        private readonly Camera _camera;
        
        public Vector2 GetSpawnPosition()
        {
            Vector2 bottomLeft = _camera.ViewportToWorldPoint(new Vector2(0, 0));
            Vector2 topRight = _camera.ViewportToWorldPoint(new Vector2(1, 1));
            return Vector2.one;
        }
    }
}