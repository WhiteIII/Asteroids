using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Services.Components
{
    public class CheckObjectForGoingOutOfLineOfSight : MonoBehaviour
    {
        [SerializeField] private Renderer _renderZone;

        private Camera _camera;

        public bool IsVisible
        {
            get
            {
                Vector3[] corners = GetBoundsCorners(_renderZone.bounds);
                int visibleCorners = 0;
                foreach (Vector3 corner in corners)
                {
                    Vector3 viewportPoint = _camera.WorldToViewportPoint(corner);
            
                    if (viewportPoint.x >= 0 && viewportPoint.x <= 1 && 
                        viewportPoint.y >= 0 && viewportPoint.y <= 1 && 
                        viewportPoint.z > 0)
                    {
                        visibleCorners++;
                    }
                }
                return visibleCorners > 0;
            }
        }
        
        [Inject] private void Construct(Camera camera) =>  
            _camera = camera;
        
        private Vector3[] GetBoundsCorners(Bounds bounds)
        {
            Vector3[] corners = new Vector3[4];
        
            corners[0] = new Vector3(bounds.min.x, bounds.min.y, bounds.center.z); 
            corners[1] = new Vector3(bounds.min.x, bounds.max.y, bounds.center.z); 
            corners[2] = new Vector3(bounds.max.x, bounds.min.y, bounds.center.z); 
            corners[3] = new Vector3(bounds.max.x, bounds.max.y, bounds.center.z);
        
            return corners;
        }
    }
}