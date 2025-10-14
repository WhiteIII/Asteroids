using _Project.Scripts.Gameplay.Services.ObjectPools.Base;
using R3;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Characters.Base
{
    public abstract class Character : 
        MonoBehaviour,
        ICharacter,
        IEnableAndDisableItem
    {
        public ReadOnlyReactiveProperty<Vector3> Position { get; private set; }

        private void Awake()
        {
            Position = Observable
                .EveryValueChanged(transform, x => x.position)
                .ToReadOnlyReactiveProperty();
            //Observable.EveryUpdate().Subscribe().AddTo(this);
            OnAwake();
        }

        public void Enable() => 
            gameObject.SetActive(true);

        public void Disable() => 
            gameObject.SetActive(false);
        
        protected virtual void OnAwake() { }
    }
}