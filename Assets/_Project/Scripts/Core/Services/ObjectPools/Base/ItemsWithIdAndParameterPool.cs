using System;
using _Project.Scripts.Core.Services.ObjectPools.Base;
using Zenject;

namespace _Project.Scripts.Core.Services.ObjectPools
{
    public abstract class ItemsWithIdAndParameterPool<TItem, TId, TParameter> : BasePoolWithId<TItem, TId>
        where TItem : IEnableAndDisableItem, IItemWithId<TId>
    {
        private readonly Action<TItem, TParameter> _actionOnGet;
        
        protected ItemsWithIdAndParameterPool(
            IFactory<TItem> factory, 
            Func<TId> idGenerator, 
            Action<TItem, TParameter> actionOnGet,
            bool disableItemOnCreate = false) : base(factory, idGenerator, disableItemOnCreate)
        {
            _actionOnGet = actionOnGet;
        }

        public TItem Get(TParameter parameter)
        {
            TItem item = GetFromPool();
            _actionOnGet.Invoke(item, parameter);
            item.Enable();
            return item;
        }
    }
}