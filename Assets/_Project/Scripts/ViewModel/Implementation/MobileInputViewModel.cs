using _Project.Scripts.ViewModel.Base;
using R3;
using UnityEngine;

namespace _Project.Scripts.ViewModel.Implementation
{
    public class MobileInputViewModel : IMobileInputViewModel
    {
        public ReadOnlyReactiveProperty<Vector2> Axis { get; private set; }
        public Observable<Unit> FireButtonClicked { get; private set; }
        public Observable<Unit> LazerFireButtonClicked { get; private set; }
        
        public void SetObservables(
            Observable<Unit> fireButtonClicked, 
            Observable<Unit> lazerFireButtonClicked,
            ReadOnlyReactiveProperty<Vector2> axis)
        {
            FireButtonClicked = fireButtonClicked;
            LazerFireButtonClicked = lazerFireButtonClicked;
            Axis = axis;
        }
    }

    public interface IMobileInputViewModel : IViewModel
    {
        ReadOnlyReactiveProperty<Vector2> Axis { get; }
        Observable<Unit> FireButtonClicked { get; }
        Observable<Unit> LazerFireButtonClicked { get; }
    }
}