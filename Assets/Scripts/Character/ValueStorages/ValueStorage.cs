using Character.ValueStorages.Bars;
using UnityEngine;

namespace Character.ValueStorages
{
    public abstract class ValueStorage
    {
        private float _currentValue;
        
        protected ValueStorage(float currentValue)
        {
            MaxValue = 0;
            CurrentValue = currentValue;
        }
        
        protected ValueStorage(float currentValue, float maxValue)
        {
            MaxValue = maxValue;
            CurrentValue = currentValue;
        }

        protected ValueStorage(float currentValue, float maxValue, ValueBar bar)
        {
            MaxValue = maxValue;
            CurrentValue = currentValue;
            Bar = bar;
        }

        public float CurrentValue
        {
            get => _currentValue;
            set => _currentValue = Mathf.Clamp(value, MinValue, MaxValue == 0 ? value : MaxValue);
            // при 0, max не ограничивается
        }

        protected float MinValue { get; } = 0;
        protected float MaxValue { get; }
        private ValueBar Bar { get; }
        
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