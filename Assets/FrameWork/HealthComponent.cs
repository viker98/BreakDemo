using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public delegate void OnHealthChangeDelegate(float newhealth, float delta, float maxHealth, GameObject instigator);
    
    public event OnHealthChangeDelegate onHealthChange;
    public event OnHealthChangeDelegate OnTakenDamge;
    public event Action OnDead;

    [SerializeField] private float _maxHealth = 100;
    private float _health;

    private void Awake()
    {
        _health = _maxHealth;

    }
    public float GetHealth()
    {
        return _health;
    }
    public float GetMaxHealth()
    {
        return _maxHealth;
    }

    public void ChangeHealth(float amt, GameObject instigator)
    {
        if (amt == 0 || _health == 0)
        {
            return;
        }
        _health = Mathf.Clamp(_health + amt, 0 , _maxHealth);

        if (amt < 0)
        {
            OnTakenDamge?.Invoke(_health, amt, _maxHealth, instigator);
        }
        onHealthChange?.Invoke(_health, amt, _maxHealth, instigator);
        if (_health <= 0)
        {
            OnDead?.Invoke();
        }
    }
}
