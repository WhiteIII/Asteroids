using System;
using _Project.Scripts.ViewModel.Implementation;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.View.Implementation
{
    public class MobileInputWindow : Window<MobileInputViewModel>
    {
        [SerializeField] private Stick _movementStick;
        [SerializeField] private Button _fireButton;
        [SerializeField] private Button _lazerFireButton;
        
        protected override void OnSetup()
        {
            Observable<Unit> fireButtonObservable = _fireButton.OnClickAsObservable();
            Observable<Unit> lazerFireButtonObservable = _lazerFireButton.OnClickAsObservable();
            ReadOnlyReactiveProperty<Vector2> axis = Observable
                .EveryValueChanged(_movementStick, x => x.Axis)
                .ToReadOnlyReactiveProperty();
            
            fireButtonObservable.Subscribe().AddTo(this);
            lazerFireButtonObservable.Subscribe().AddTo(this);
            axis.Subscribe().AddTo(this);
            
            ViewModel.SetObservables(fireButtonObservable, lazerFireButtonObservable, axis);
        }
    }
}