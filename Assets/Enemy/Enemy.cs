using System;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    private HealthComponent _healthComponent;
    private Animator _animator;
    private static readonly int DeadID = Animator.StringToHash("Dead");

    private void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _healthComponent.OnTakenDamge += takeDamage;
        _healthComponent.OnDead += StartDeath;
        _animator = GetComponent<Animator>();
    }

    private void StartDeath()
    {
        _animator.SetTrigger(DeadID);
    }
    public void DeathAnimationEvent()
    {
        Destroy(gameObject);
    }

    private void takeDamage(float newhealth, float delta, float maxHealth, GameObject instigator)
    {
        Debug.Log($"I took {delta} amt of damage, health is now {newhealth}/{maxHealth}");
    }
}
