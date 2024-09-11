using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private HealthComponent _healthComponent;

    private void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _healthComponent.OnTakenDamge += takeDamage;
        _healthComponent.OnDead += StartDeath;
    }

    private void StartDeath()
    {
        Destroy(gameObject);
    }

    private void takeDamage(float newhealth, float delta, float maxHealth)
    {
        Debug.Log($"I took {delta} amt of damage, health is now {newhealth}/{maxHealth}");
    }
}
