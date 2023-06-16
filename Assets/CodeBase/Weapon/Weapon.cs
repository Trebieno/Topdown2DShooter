using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoCache
{
    [SerializeField] private int _level;
    [SerializeField] private float _damage;
    [SerializeField] private float _spread;
    [SerializeField] private float _delayTime;
    [SerializeField] private float _reloadTime;
    [SerializeField] private float _weight;
    [SerializeField] private float _speedBullet;

    [SerializeField] private int _bullet;
    [SerializeField] private int _maxBullet;
    [SerializeField] private Transform _firePoint;

    public float DelayTime => _delayTime;
    public float ReloadTime => _reloadTime;
    public float Weight => _weight;

    public event Action Shooted;
    public event Action Reloaded;


    [SerializeField] private Bullet _bulletPrefab;

    private float _maxDelayTime;
    private float _maxReloadTime;

    public float Damage => _damage;
    public float Spread => _spread;

    private void Start()
    {
        _maxDelayTime = _delayTime;
        _maxReloadTime = _reloadTime;
    }

    public override void OnFixedUpdateTick()
    {
        if(_delayTime > 0)
            _delayTime -= Time.fixedDeltaTime;
        
        if(_reloadTime > 0)
        {
            _reloadTime -= Time.fixedDeltaTime;
            if(_reloadTime <= 0)
                Reload();
        }
    }

    public void Shoot()
    {
        if(_bullet <= 0)
        {
            _reloadTime = _maxReloadTime;
            return;
        }

        var bullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
        bullet.StartMove(_speedBullet, 2, _spread, _damage);
        _bullet -= 1;
        _delayTime = _maxDelayTime;
        Shooted?.Invoke();
    }

    public void Reload()
    {
        _bullet = _maxBullet;
        Reloaded?.Invoke();
    }

    public void Upgrade()
    {

    }

    public void UpgradeDamage()
    {

    }

    public void UpgradeSpread()
    {
        
    }

    public void UpgradeDelay()
    {
        
    }

    public void UpgradeReload()
    {
        
    }
}
