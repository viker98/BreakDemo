using System;
using Unity.Behavior;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour, IBehaviorInterface, ITeamInterface
{
    [SerializeField] private int TeamID = 1;
    private HealthComponent _healthComponent;
    private Animator _animator;
    private static readonly int DeadID = Animator.StringToHash("Dead");
    private PerceptionComponent _perceptionComponent;
    private BehaviorGraphAgent _behaviorGraphAgent;

    public int GetTeamID()
    {
        return TeamID;
    }

    private void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _healthComponent.OnTakenDamge += takeDamage;
        _healthComponent.OnDead += StartDeath;
        _animator = GetComponent<Animator>();
        _perceptionComponent = GetComponent<PerceptionComponent>();
        _perceptionComponent.OnPerceptionTargetUpdated += HandleTargetUpdate;
        _behaviorGraphAgent = GetComponent<BehaviorGraphAgent>();
    }

    private void HandleTargetUpdate(GameObject target, bool bIsSensed)
    {
        if(bIsSensed)
        {
            _behaviorGraphAgent.BlackboardReference.SetVariableValue("Target", target);
        }
        else
        {
            _behaviorGraphAgent.BlackboardReference.SetVariableValue<GameObject>("Target", null);
            _behaviorGraphAgent.BlackboardReference.SetVariableValue("checkoutLocation", target.transform.position);
            _behaviorGraphAgent.BlackboardReference.SetVariableValue("hasCheckLocation", true);
        }
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

    public void Attack(GameObject target)
    {
        _animator.SetTrigger("Attack");
    }
}
