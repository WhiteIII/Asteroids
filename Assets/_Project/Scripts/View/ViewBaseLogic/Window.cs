using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.View.ViewBaseLogic
{
    public abstract class Window<T> : MonoBehaviour, IWindow<T>
    {
        protected T ViewModel { get; private set; }
        
        public void Setup(T viewModel)
        {
            ViewModel = viewModel;
        }

        public UniTask Open()
        {
            throw new System.NotImplementedException();
        }

        public UniTask Close()
        {
            throw new System.NotImplementedException();
        }
    }
}