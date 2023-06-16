using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoCache
{
    [SerializeField] private List<Weapon> _allWeapons = new List<Weapon>();
    [SerializeField] private CameraShake _shake;
    private Weapon _weapon => _allWeapons[index];
    private bool _isFire => Input.GetButton("Fire1");
    private int index = 0;

    public event Action<float> SelectedGun;

    public override void OnUpdateTick()
    {
        if(_isFire && _weapon.DelayTime <= 0 && _weapon.ReloadTime <= 0)
        {
            _weapon.Shoot();
            _shake.Shake();
        }
        
        ScrollingWeapon();
    }
    private void ScrollingWeapon()
    {
        var scrollView = Input.GetAxis("Mouse ScrollWheel");
        int i = index;
        if(scrollView < 0)
            index++;

        if(scrollView > 0)
            index--;
        
        if(index > _allWeapons.Count - 1)
            index = 0;
        
        if(index < 0)
            index = _allWeapons.Count - 1;
        
        if(i != index)
        {
            SelectedGun.Invoke(_weapon.Weight);
            // TakeGun();
            // _sliderReload.maxValue = MaxCulldownReload;
            // _sliderReload.value = CurCulldownReload;
            
            // if(CurCulldownReload >= MaxCulldownReload)
            //     _sliderReload.gameObject.SetActive(false);
        }
            
    }
}
