using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Healing _healing;
    public Healing Healing => _healing;

    private void Start()
    {
        EnemyAll.Instance.Enemies.Add(gameObject, this);
    }
}
