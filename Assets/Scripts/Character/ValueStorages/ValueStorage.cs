using Character.ValueStorages.Bars;
using UnityEngine;

namespace Character.ValueStorages
{
    public abstract class ValueStorage
    {
        private float _currentValue;
        
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

        public float CurrentValue
        {
            get => _currentValue;
            set { if (value <= MaxValue && value >= MinValue) _currentValue = value; }
        }

        protected float MinValue { get; } = 0;
        protected float MaxValue { get; }
        
        public virtual void Increase(float value) => AddValue(value);
        public virtual void Decrease(float value) => AddValue(-value);

        private void AddValue(float value)
        {
            CurrentValue += value;
            ChangeBar();
        }

        private void ChangeBar()
        {
            if (Bar != null) Bar.SetCurrentValue(GetPercentageRation());
        }

        public float GetPercentageRation() => CurrentValue / MaxValue * 100;
    }
}