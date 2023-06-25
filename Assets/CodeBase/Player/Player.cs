using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoCache
{
    [SerializeField] private Movement _movement;
    [SerializeField] private Shooting _shooting;
    [SerializeField] private Healing _healing;

    public Movement Movement => _movement;
    public Shooting Shooting => _shooting;
    public Healing Healing => _healing;

    private void Start()
    {
        // PlayerCache.Instance.Players.Add(gameObject, this);
        PlayerCache.Instance.Player = this;
        _shooting.SelectedGun += _movement.SetSpeedWeight;
    }

    private void OnDestroy()
    {
        _shooting.SelectedGun -= _movement.SetSpeedWeight;
    }
}
