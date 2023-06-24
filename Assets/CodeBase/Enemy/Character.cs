using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Character : MonoCache
{
    [SerializeField] private Healing _healing;

    [SerializeField] private float _radiusFov;
    [SerializeField] private float _angleFov;
    [SerializeField] private LayerMask _enemyMask;
    [SerializeField] private AIDestinationSetter _agent;

    public Healing Healing => _healing;


    private void Start()
    {
        EnemyAll.Instance.Enemies.Add(gameObject, this);
    }

    public override void OnFixedUpdateTick()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, _radiusFov, _enemyMask);
        for (int i = 0; i < targets.Length; i++)
        {
            Vector2 direction = targets[i].transform.position - transform.position;
            float angleBetween = Vector3.SignedAngle(direction, transform.up, Vector2.up);
            if(_agent.target == null && angleBetween <= _angleFov)
            {
                _agent.target = targets[i].transform;
            }
            else
                _agent.target = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radiusFov);
    }
}
