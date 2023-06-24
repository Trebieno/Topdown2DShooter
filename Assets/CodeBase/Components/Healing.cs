using System;
using UnityEngine;

public class Healing : MonoBehaviour {

	[SerializeField] private float _currentHealth;
	[SerializeField] private float _maximunHealth;

	[SerializeField] private ParticleSystem _deathEffectPrefab;
	[SerializeField] private ParticleSystem _healEffectPrefab;
	[SerializeField] private ParticleSystem _damageEffectPrefab;

	public event Action Died;
	public event Action Damaged;
	public event Action HealHealth;
    public event Action<float, float> HealthChanged;


	
	public float CurrentHealth 
	{
		get{ return _currentHealth;}
	 	private set
		{
			_currentHealth = value; 
			HealthChanged?.Invoke(_currentHealth, _maximunHealth);
		}
	}
	
	public float MaximunHealth => _maximunHealth;


	public void SetDamage(float damage, Vector3 damagedPosition)
	{
		_currentHealth -= damage;
		if(_currentHealth <= 0)
			Death();

		if(_deathEffectPrefab != null)
			Instantiate(_deathEffectPrefab, damagedPosition, Quaternion.identity);
			
		Damaged?.Invoke();
	}

	public virtual void Death()
	{
		if(_deathEffectPrefab != null)
			Instantiate(_deathEffectPrefab, transform.position, Quaternion.identity);
			
		gameObject.SetActive(false);

		Died?.Invoke();
	}	

	public virtual void Heal(float health)
	{
		if(_healEffectPrefab != null)
			Instantiate(_healEffectPrefab, transform.position, Quaternion.identity);

		CurrentHealth += health;

		if(CurrentHealth > MaximunHealth)
			CurrentHealth = MaximunHealth;

		HealHealth.Invoke();
	}
}
