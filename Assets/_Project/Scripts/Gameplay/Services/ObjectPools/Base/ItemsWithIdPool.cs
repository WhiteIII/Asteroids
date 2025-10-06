using System;
using _Project.Scripts.Gameplay.Services.ObjectPools.Base;
using Zenject;

namespace _Project.Scripts.Gameplay.Services.ObjectPools
{
    public abstract class ItemsWithIdPool<TItem, TId> : BasePoolWithId<TItem, TId>
        where TItem : IEnableAndDisableItem, IItemWithId<TId>
    {
        protected ItemsWithIdPool(
            Func<TItem> createMethod,
            Func<TId> idGenerator, 
            bool disableItemOnCreate = false) : base(createMethod, idGenerator, disableItemOnCreate)
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
