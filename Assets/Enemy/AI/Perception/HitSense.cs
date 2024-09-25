using System;
using UnityEngine;

public class HitSense : Sense
{
    private void Awake()
    {
        HealthComponent healthComp = GetComponent<HealthComponent>();
        if (healthComp)
        {
            healthComp.OnTakenDamge += HandleDamageEvent;
        }
    }

    private void HandleDamageEvent(float newhealth, float delta, float maxHealth, GameObject instigator)
    {
        Stimuli instigatorStimuli = instigator.GetComponent<Stimuli>();
        if (instigatorStimuli)
        {
            HandleSensibleStimuli(instigatorStimuli);
        }
    }

    protected override bool IsStimuliSensable(Stimuli stimuli)
    {
        return false;
    }
}
