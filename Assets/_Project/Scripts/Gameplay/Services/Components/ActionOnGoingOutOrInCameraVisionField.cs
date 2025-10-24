using System;
using _Project.Scripts.Gameplay.Characters.Base;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Services.Components
{
    public class ActionOnGoingOutOrInCameraVisionField : MonoBehaviour
    {
        private Character _character;
        private Action _actionOnGoingInCameraVisionField;
        private Action _actionOnGoingOutCameraVisionField;

        private bool _actionOnGoingInCameraVisionFieldIsInvoked;
        private bool _actionOnGoingOutCameraVisionFieldIsInvoked;
        
        public void Initialize(
            Action actionOnGoingInCameraVisionField = null,
            Action actionOnGoingOutCameraVisionField = null)
        {
            _actionOnGoingInCameraVisionField = actionOnGoingInCameraVisionField;
            _actionOnGoingOutCameraVisionField = actionOnGoingOutCameraVisionField;
        }
        
        private void Awake() =>
            _character = GetComponent<Character>();

        private void Update()
        {
            if (_actionOnGoingInCameraVisionFieldIsInvoked && _character.IsVisible)
                return;
            if (_actionOnGoingOutCameraVisionFieldIsInvoked && _character.IsVisible == false)
                return;

            if (_character.IsVisible)
            {
                _actionOnGoingInCameraVisionField?.Invoke();
                _actionOnGoingInCameraVisionFieldIsInvoked = true;
                _actionOnGoingOutCameraVisionFieldIsInvoked = false;
            }
            else
            {
                _actionOnGoingOutCameraVisionField?.Invoke();
                _actionOnGoingOutCameraVisionFieldIsInvoked = true;
                _actionOnGoingInCameraVisionFieldIsInvoked = false;
            }
        }
    }
}