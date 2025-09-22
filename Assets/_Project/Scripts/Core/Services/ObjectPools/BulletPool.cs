using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Core.ShootingSystem;
using R3;
using Zenject;

public class BulletPool : IDisposable
{
    private readonly IFactory<Bullet> _bulletFactory;
    private readonly Dictionary<string, Bullet> _enabledBulletsDictionary = new();   
    private readonly Dictionary<string, Bullet> _disabledBulletsDictionary = new();
    private readonly CompositeDisposable  _disposables = new();

    public BulletPool(IFactory<Bullet> bulletFactory) =>
        _bulletFactory = bulletFactory;

    public void Dispose() =>
        _disposables.Dispose();

    public Bullet Get()
    {
        if (_disabledBulletsDictionary.Count == 0)
        {
            Bullet bullet = CreateAndAddBulletOnEnabledBulletsDictionary();
            bullet
                .OnHit
                .Subscribe(Release)
                .AddTo(_disposables);
            bullet.Enable();
            return bullet;
        }

        KeyValuePair<string, Bullet> bulletAndId = _disabledBulletsDictionary.First();
        _disabledBulletsDictionary.Remove(bulletAndId.Key);
        _enabledBulletsDictionary.Add(bulletAndId.Key, bulletAndId.Value);
        bulletAndId.Value.Enable();
        return bulletAndId.Value;
    }

    private void Release(string id)
    {
        _disabledBulletsDictionary.Add(id, _enabledBulletsDictionary[id]);
        _enabledBulletsDictionary.Remove(id);
        _disabledBulletsDictionary[id].Disable();
    }

    private Bullet CreateAndAddBulletOnEnabledBulletsDictionary()
    {
        Bullet bullet = _bulletFactory.Create();
        string id = Guid.NewGuid().ToString();
        bullet.SetID(id);
        _enabledBulletsDictionary.Add(id, bullet);
        return bullet;
    }
}
