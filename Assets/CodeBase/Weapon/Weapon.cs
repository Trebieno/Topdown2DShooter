using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoCache
{
    [SerializeField] private String _name;
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

    public string Name => _name;
    public int Level => _level;
    public float Damage => _damage;
    public float Spread => _spread;
    public float DelayTime => _delayTime;
    public float MaxDelayTime => _maxDelayTime;
    public float ReloadTime => _reloadTime;
    public float Weight => _weight;
    public float SpeedBullet => _speedBullet;

    public int Bullet => _bullet;
    public int MaxBullet => _maxBullet;

    public event Action Shooted;
    public event Action Reloaded;


    [SerializeField] private Bullet _bulletPrefab;

    private float _maxDelayTime;
    private float _maxReloadTime;


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

    public WeaponData ConvertToWeaponData()
    {
        WeaponData weaponData = new WeaponData();
        weaponData.Name = _name;
        weaponData.Level = _level;
        weaponData.Damage = _damage;
        weaponData.Spread = _spread;

        weaponData.DelayTime = _delayTime;
        weaponData.MaxDelayTime = _maxDelayTime;

        weaponData.ReloadTime = _reloadTime;
        weaponData.MaxRealoadTime = _maxReloadTime;

        weaponData.Weight = _weight;
        weaponData.SpeedBullet = _speedBullet;
        weaponData.Bullet = _bullet;
        weaponData.MaxBullet = _maxBullet;
        return weaponData;
    }

    public void SetParameters(WeaponData weaponData)
    {
        _name = weaponData.Name;
        _level = weaponData.Level;
        _damage = weaponData.Damage;
        _spread = weaponData.Spread;

        _delayTime = weaponData.DelayTime;
        _maxDelayTime = weaponData.MaxDelayTime;

        _reloadTime = weaponData.ReloadTime;
        _maxReloadTime = weaponData.MaxRealoadTime;

        _weight = weaponData.Weight;
        _speedBullet = weaponData.SpeedBullet;
        _bullet = weaponData.Bullet;
        _maxBullet = weaponData.MaxBullet;

    }
}
