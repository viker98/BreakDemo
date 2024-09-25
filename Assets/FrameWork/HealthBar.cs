using System;
using UnityEngine;

public class HealthBar : ValueGauge
{
    public override void SetOwner(GameObject newOwner)
    {
        base.SetOwner(newOwner);
        HealthComponent ownerHealthComponent = newOwner.GetComponent<HealthComponent>();
        if (ownerHealthComponent)
        {
            ownerHealthComponent.onHealthChange += HealthChanged;
            ownerHealthComponent.OnDead += OwnerDead;
        }
    }

    private void OwnerDead()
    {
        Destroy(gameObject);
    }

    private void HealthChanged(float newHealth, float delta, float maxHealth, GameObject instigator)
    {
        UpdateValue(newHealth, maxHealth);
    }
}
