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
        
        protected ValueStorage(float currentValue, ViewBar bar)
        {
            MaxValue = 0;
            CurrentValue = currentValue;
            Bar = bar;
        }
        
        protected ValueStorage(float currentValue, float maxValue)
        {
            MaxValue = maxValue;
            CurrentValue = currentValue;
        }

        protected ValueStorage(float currentValue, float maxValue, ViewBar bar)
        {
            MaxValue = maxValue;
            CurrentValue = currentValue;
            Bar = bar;
        }

        public float CurrentValue
        {
            get => _currentValue;
            protected set
            {
                _currentValue = Mathf.Clamp(value, MinValue, MaxValue == 0 ? value : MaxValue);
                ChangeBar();
            }
            // при 0, max не ограничивается
        }

        protected float MinValue { get; } = 0;
        protected float MaxValue { get; }
        protected ViewBar Bar { get; }

        public virtual void Increase(float value) => CurrentValue += value;
        public virtual void Decrease(float value) => CurrentValue -= value;

        protected virtual void ChangeBar()
        {
            if (Bar != null) Bar.SetCurrentValue(GetPercentageRation());
        }

        public float GetPercentageRation() => CurrentValue / MaxValue * 100;
    }
}