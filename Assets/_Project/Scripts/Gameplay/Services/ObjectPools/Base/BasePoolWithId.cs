using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Gameplay.Services.ObjectPools.Base;
using R3;
using Zenject;

namespace _Project.Scripts.Gameplay.Services.ObjectPools
{
    public abstract class BasePoolWithId<TItem, TId> : IDisposable
        where TItem : IEnableAndDisableItem, IItemWithId<TId>
    {
        private readonly IFactory<TItem> _factory;
        private readonly Func<TId> _idGenerator;
        private readonly Action<TItem> _onGet;
        private readonly Action<TItem> _onRelease;
        private readonly bool _disableItemOnCreate;
        private readonly Dictionary<TId, TItem> _enabledItemsDictionary = new();   
        private readonly Dictionary<TId, TItem> _disabledItemsDictionary = new();
        private readonly CompositeDisposable  _disposables = new();

        protected BasePoolWithId(
            IFactory<TItem> factory, 
            Func<TId> idGenerator,  
            bool disableItemOnCreate = false,
            Action<TItem> onGet = null,
            Action<TItem> onRelease = null)
        {
            _factory = factory;
            _idGenerator = idGenerator;
            _disableItemOnCreate = disableItemOnCreate;
            _onGet = onGet;
            _onRelease = onRelease;
        }

        public void Dispose() => 
            _disposables.Dispose();

        protected TItem GetFromPool()
        {
            if (_disabledItemsDictionary.Count == 0)
            {
                TItem item = CreateItemAndAddInEnabledItemsDictionary();
                _onGet?.Invoke(item);
                item
                    .Release
                    .Subscribe(Release)
                    .AddTo(_disposables);
                return item;
            }

            KeyValuePair<TId, TItem> itemAndId = _disabledItemsDictionary.First();
            _disabledItemsDictionary.Remove(itemAndId.Key);
            _enabledItemsDictionary.Add(itemAndId.Key, itemAndId.Value);
            _onGet?.Invoke(itemAndId.Value);
            return itemAndId.Value;
        }
        
        private TItem CreateItemAndAddInEnabledItemsDictionary()
        {
            TId id = _idGenerator();
            TItem item = _factory.Create();
            if (_disableItemOnCreate)
                item.Disable();
            item.SetID(id);
            _enabledItemsDictionary.Add(id, item);
            return item;
        }
        
        private void Release(TId id)
        {
            _disabledItemsDictionary.Add(id, _enabledItemsDictionary[id]);
            _onRelease?.Invoke(_enabledItemsDictionary[id]);
            _enabledItemsDictionary.Remove(id);
            _disabledItemsDictionary[id].Disable();
        }
    }
}