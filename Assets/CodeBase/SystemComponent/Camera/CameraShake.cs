using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoCache
{
    [SerializeField] private float _shakeDuration = 0f;
    [SerializeField] private float _shakeAmount = 0.7f;
    [SerializeField] private float _decreaseFactor = 1.0f;

    private Vector3 _originalPos;

    void Start()
    {
        _originalPos = transform.localPosition;
    }

    public override void OnFixedUpdateTick()
    {            
        if (_shakeDuration > 0)
        {
            transform.localPosition = transform.localPosition + Random.insideUnitSphere * _shakeAmount;

            _shakeDuration -= Time.fixedDeltaTime * _decreaseFactor;
        }
        else
        {
            _shakeDuration = 0f;
        }
    }

    public void Shake()
    {
        _shakeDuration = 0.2f;
    }
}
