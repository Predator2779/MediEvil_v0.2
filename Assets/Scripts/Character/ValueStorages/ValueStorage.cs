using Character.ValueStorages.Bars;
using UnityEngine;

namespace Character.ValueStorages
{
    public abstract class ValueStorage
    {
        protected ValueStorage(float currentValue, float maxValue)
        {
            CurrentValue = currentValue;
            MaxValue = maxValue;
        }

        protected ValueStorage(float currentValue, float maxValue, ValueBar bar)
        {
            CurrentValue = currentValue;
            MaxValue = maxValue;
            Bar = bar;
        }

        private ValueBar Bar { get; }
        protected float MinValue { get; } = 0;
        public float CurrentValue { get; private set; }
        protected float MaxValue { get; }

        public virtual void Increase(float value) =>
            SetValue(Mathf.Clamp(CurrentValue + value, CurrentValue, MaxValue));

        public virtual void Decrease(float value) =>
            SetValue(Mathf.Clamp(CurrentValue - value, MinValue, CurrentValue));

        private void SetValue(float value)
        {
            CurrentValue = value;
            ChangeBar();
        }

        private void ChangeBar()
        {
            if (Bar != null) Bar.SetCurrentValue(GetPercentageRation());
        }

        public float GetPercentageRation() => CurrentValue / MaxValue * 100;
    }
}