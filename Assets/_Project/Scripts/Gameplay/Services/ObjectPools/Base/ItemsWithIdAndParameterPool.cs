using System;
using _Project.Scripts.Gameplay.Services.ObjectPools.Base;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.Scripts.Gameplay.Services.ObjectPools
{
    public abstract class ItemsWithIdAndParameterPool<TItem, TId, TParameter> : BasePoolWithId<TItem, TId>
        where TItem : IEnableAndDisableItem, IItemWithId<TId>
    {
        private readonly Action<TItem, TParameter> _actionOnGetWithParameter;
        
        protected ItemsWithIdAndParameterPool(
            Func<UniTask<TItem>> createMethod, 
            Func<TId> idGenerator, 
            Action<TItem, TParameter> actionOnGetWithParameter,
            bool disableItemOnCreate = false,
            Action<TItem> onRelease = null) : 
            base(createMethod, idGenerator, disableItemOnCreate, null, onRelease)
        {
            _actionOnGetWithParameter = actionOnGetWithParameter;
        }

        public async UniTask<TItem> Get(TParameter parameter)
        {
            TItem item = await GetFromPool();
            _actionOnGetWithParameter.Invoke(item, parameter);
            item.Enable();
            return item;
        }
    }
}