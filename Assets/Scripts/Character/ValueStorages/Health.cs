using Character.ValueStorages.Bars;
using Global;

namespace Character.ValueStorages
{
    public class Health : ValueStorage
    {
        public delegate void HealthEvent();
        public HealthEvent Falldown;
        public HealthEvent Die;

        public Health(float currentValue, float maxValue, ValueBar bar) : base(currentValue, maxValue, bar)
        {
        }

        public bool CanDamage { get; set; } = true;

        public void TakeFullHeal() => CurrentValue = MaxValue;

        public override void Decrease(float value)
        {
            if (!CanDamage) return;

            base.Decrease(value);
            
            if (value >= MaxValue * GlobalConstants.KnockdownDamage) Falldown?.Invoke();
            if (CurrentValue <= MinValue) Die?.Invoke();
        }
    }
}