using UnityEngine;

public class HitSense : Sense
{
    protected override bool IsStimuliSensable(Stimuli stimuli)
    {
        return false;
    }
}
