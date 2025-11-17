using System;
using _Project.Scripts.Gameplay.Services.ObjectPools.Base;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Gameplay.Services.ObjectPools
{
    public abstract class ItemsWithIdPool<TItem, TId> : BasePoolWithId<TItem, TId>
        where TItem : IEnableAndDisableItem, IItemWithId<TId>
    {
        protected ItemsWithIdPool(
            Func<UniTask<TItem>> createMethod,
            Func<TId> idGenerator, 
            bool disableItemOnCreate = false) : base(createMethod, idGenerator, disableItemOnCreate)
        {
        }

        public async UniTask<TItem> Get()
        {
            TItem item = await GetFromPool();
            item.Enable();
            return item;
        } 
    }
}
