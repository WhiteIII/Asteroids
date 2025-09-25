using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Core.Services.ObjectPools.Base;
using R3;
using Zenject;

namespace _Project.Scripts.Core.Services.ObjectPools
{
    public abstract class ItemsWithIdPool<TItem, TId> : IDisposable
        where TItem : IEnableAndDisableItem, IItemWithId<TId>
    {
        private readonly IFactory<TItem> _factory;
        private readonly Func<TId> _idGenerator;
        private readonly Dictionary<TId, TItem> _enabledItemsDictionary = new();   
        private readonly Dictionary<TId, TItem> _disabledItemsDictionary = new();
        private readonly CompositeDisposable  _disposables = new();

        protected ItemsWithIdPool(IFactory<TItem> factory, Func<TId> idGenerator)
        {
            _factory = factory;
            _idGenerator = idGenerator;
        }

        public void Dispose() =>
            _disposables.Dispose();

        public TItem Get()
        {
            if (_disabledItemsDictionary.Count == 0)
            {
                TItem item = CreateItemAndAddInEnabledItemsDictinary();
                item
                    .Release
                    .Subscribe(Release)
                    .AddTo(_disposables);
                item.Enable();
                return item;
            }

            KeyValuePair<TId, TItem> itemAndId = _disabledItemsDictionary.First();
            _disabledItemsDictionary.Remove(itemAndId.Key);
            _enabledItemsDictionary.Add(itemAndId.Key, itemAndId.Value);
            itemAndId.Value.Enable();
            return itemAndId.Value;
        }

        private void Release(TId id)
        {
            _disabledItemsDictionary.Add(id, _enabledItemsDictionary[id]);
            _enabledItemsDictionary.Remove(id);
            _disabledItemsDictionary[id].Disable();
        }

        private TItem CreateItemAndAddInEnabledItemsDictinary()
        {
            TItem item = _factory.Create();
            TId id = _idGenerator();
            item.SetID(id);
            _enabledItemsDictionary.Add(id, item);
            return item;
        }
    }
}