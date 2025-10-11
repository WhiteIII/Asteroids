using UnityEngine;

namespace _Project.Scripts.View.Services
{
    public class UIRoot : MonoBehaviour
    {
        public void AddWindow(Transform windowTransform) => 
            windowTransform.SetParent(transform);
    }
}