using UnityEngine;
using UnityEngine.UI;

public abstract class ValueGauge : Widget
{
    [SerializeField] Slider slider;

    public void UpdateValue(float newValue, float newMaxValue)
    {
        if(newMaxValue == 0)
        {
            return;
        }

        slider.value = newValue / newMaxValue;
    }
}
