using System;
using _Project.Scripts.Core.Services.ObjectPools.Base;
using Zenject;

namespace _Project.Scripts.Core.Services.ObjectPools
{
    public abstract class ItemsWithIdPool<TItem, TId> : BasePoolWithId<TItem, TId>
        where TItem : IEnableAndDisableItem, IItemWithId<TId>
    {
        protected ItemsWithIdPool(
            IFactory<TItem> factory,
            Func<TId> idGenerator, 
            bool disableItemOnCreate = false) : base(factory, idGenerator, disableItemOnCreate)
        {
        }

        public TItem Get()
        {
            TItem item = GetFromPool();
            item.Enable();
            return item;
        } 
    }
}
