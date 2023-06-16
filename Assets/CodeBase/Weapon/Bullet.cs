using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _damage;

    public void StartMove(float speedBullet, float destroyTime, float maxSpread, float damage)
    {
        float spread = Random.Range(-maxSpread, maxSpread);
        Vector2 bulletDirection = Quaternion.Euler(0, 0, spread) * transform.up;
        _rb.AddForce(bulletDirection * speedBullet, ForceMode2D.Impulse);

        Destroy(gameObject, destroyTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
            EnemyAll.Instance.Enemies[other.gameObject].Healing.SetDamage(_damage, transform.position);

        Destroy(gameObject);
    }
}
