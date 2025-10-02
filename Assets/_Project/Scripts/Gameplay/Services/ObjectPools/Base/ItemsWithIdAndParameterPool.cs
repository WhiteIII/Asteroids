using System;
using _Project.Scripts.Gameplay.Services.ObjectPools.Base;
using Zenject;

namespace _Project.Scripts.Gameplay.Services.ObjectPools
{
    public abstract class ItemsWithIdAndParameterPool<TItem, TId, TParameter> : BasePoolWithId<TItem, TId>
        where TItem : IEnableAndDisableItem, IItemWithId<TId>
    {
        private readonly Action<TItem, TParameter> _actionOnGetWithParameter;
        
        protected ItemsWithIdAndParameterPool(
            IFactory<TItem> factory, 
            Func<TId> idGenerator, 
            Action<TItem, TParameter> actionOnGetWithParameter,
            bool disableItemOnCreate = false,
            Action<TItem> onRelease = null) : 
            base(factory, idGenerator, disableItemOnCreate, null, onRelease)
        {
            _actionOnGetWithParameter = actionOnGetWithParameter;
        }

        public TItem Get(TParameter parameter)
        {
            TItem item = GetFromPool();
            _actionOnGetWithParameter.Invoke(item, parameter);
            item.Enable();
            return item;
        }
    }
}