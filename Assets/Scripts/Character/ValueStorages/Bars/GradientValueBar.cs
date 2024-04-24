using UnityEngine;

namespace Character.ValueStorages.Bars
{
    public class GradientValueBar : ValueBar
    {
        [SerializeField] private Gradient gradient;

        public override void SetCurrentValue(float value)
        {
            base.SetCurrentValue(value);
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }
    }
}