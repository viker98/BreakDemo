using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public delegate void OnHealthChangeDelegate(float newhealth, float delta, float maxHealth);
    
    public event OnHealthChangeDelegate onHealthChange;
    public event OnHealthChangeDelegate OnTakenDamge;
    public event Action OnDead;

    [SerializeField] private float maxHealth = 100;
    private float _health;

    private void Awake()
    {
        _health = maxHealth;

    }

    public void ChangeHealth(float amt)
    {
        if (amt == 0 || _health == 0)
        {
            return;
        }
        _health = Mathf.Clamp(_health + amt, 0 , maxHealth);

        if (amt < 0)
        {
            OnTakenDamge?.Invoke(_health, amt, maxHealth);
        }
        onHealthChange?.Invoke(_health, amt, maxHealth);
        if (_health <= 0)
        {
            OnDead.Invoke();
        }
    }
}
