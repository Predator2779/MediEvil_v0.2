using Character.ComponentContainer;
using Character.ValueStorages.Bars;
using Global;

namespace Character.ValueStorages
{
    public class Health : ValueStorage
    {
        public delegate void HealthEvent();
        public HealthEvent Falldown;
        public HealthEvent Die;

        public Health(PersonContainer personContainer, float currentValue, float maxValue, ValueBar bar) : base(currentValue, maxValue, bar)
        {
            PersonContainer = personContainer;
        }

        private PersonContainer PersonContainer { get; }
        public bool CanDamage { get; set; } = true;

        public void TakeHeal(float value) => Increase(value);
        public void TakeFullHeal() => Increase(MaxValue);

        public void TakeDamage(float value)
        {
            if (!CanDamage) return;
            
            base.Decrease(value);
            
            if (value >= MaxValue * GlobalConstants.KnockdownDamage) Falldown?.Invoke();
            if (CurrentValue <= MinValue) Die?.Invoke();
        }
    }
}