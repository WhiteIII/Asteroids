using _Project.Scripts.Data.Implementation;
using _Project.Scripts.Data.Services.Repositories.Base;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Services.Spawners
{
    public class SpawnPositionHelper : ISpawnPositionHelper
    {
        private readonly Camera _camera;
        private readonly float _offsetOnCameraBoard;

        public SpawnPositionHelper(Camera camera, IDataRepository dataRepository)
        {
            _camera = camera;
            _offsetOnCameraBoard = dataRepository.GetData<GameSettingsConfig>().SpawnOffsetOutSideCameraVision;
        }

        public Vector2 GetSpawnPosition()
        {
            int side = Random.Range(0, 4);
            Vector2 bottomLeft = _camera.ViewportToWorldPoint(new Vector2(0, 0));
            Vector2 bottomRight = _camera.ViewportToWorldPoint(new Vector2(1, 0));
            Vector2 topRight = _camera.ViewportToWorldPoint(new Vector2(1, 1));
            float halfCameraVisionHeight = topRight.y / 2f; 
            float halfCameraVisionLenght = topRight.x / 2f; 

            return side switch
            {
                0 => new Vector2(
                    Random.Range(bottomLeft.x, bottomRight.x), 
                    halfCameraVisionHeight + _offsetOnCameraBoard),
                1 => new Vector2(
                    Random.Range(bottomLeft.x, bottomRight.x), 
                    -(halfCameraVisionHeight + _offsetOnCameraBoard)),
                2 => new Vector2(
                    halfCameraVisionLenght + _offsetOnCameraBoard, 
                    Random.Range(bottomLeft.y, topRight.y)),
                3 => new Vector2(
                    -(halfCameraVisionLenght + _offsetOnCameraBoard), 
                    Random.Range(bottomLeft.y, topRight.y)),
                _ => Vector2.zero
            };
        }
    }

    public interface ISpawnPositionHelper
    {
        Vector2 GetSpawnPosition();
    }
}