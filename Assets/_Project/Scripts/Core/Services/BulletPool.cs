using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Core.ShootingSystem;
using R3;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core.Services
{
    public class BulletPool : IDisposable
    {
        private readonly IFactory<string, Transform, Bullet> _factory;
        private readonly Transform _bulletsParent;
        private readonly Dictionary<string, Bullet> _enableBulletsDictionary = new();
        private readonly Dictionary<string, Bullet> _disableBulletsDictionary = new();
        private readonly CompositeDisposable _disposable = new();
        
        public void Dispose()
        {
            foreach (Bullet bullet in _enableBulletsDictionary.Values)
                bullet.Dispose();
            foreach (Bullet bullet in _disableBulletsDictionary.Values)
                bullet.Dispose();
            _disposable.Dispose();
        }
        
        public Bullet Get()
        {
            if (_disableBulletsDictionary.Count == 0)
            {
                Bullet bullet = CreateAndAddInEnableBulletsDictionary();
                bullet
                    .OnTouchTarget
                    .Subscribe(Release)
                    .AddTo(_disposable);
                bullet.Enable();
                return bullet;
            }

            KeyValuePair<string, Bullet> dictionaryItem = _disableBulletsDictionary.First();
            _disableBulletsDictionary.Remove(dictionaryItem.Key);
            _enableBulletsDictionary.Add(dictionaryItem.Key, dictionaryItem.Value);
            
            return dictionaryItem.Value;
        }

        private void Release(string id)
        {
            
        }

        private Bullet CreateAndAddInEnableBulletsDictionary()
        {
            string id = Guid.NewGuid().ToString();
            Bullet bullet = _factory.Create(id, _bulletsParent);
            _enableBulletsDictionary.Add(id, bullet);
            return bullet;
        } 
    }
}