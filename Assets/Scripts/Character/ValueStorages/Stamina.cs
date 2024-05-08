using System.Threading.Tasks;
using Character.ComponentContainer;
using Character.ValueStorages.Bars;

namespace Character.ValueStorages
{
    public class Stamina : ValueStorage
    {
        private int _staminaRestoreDelay;
        private bool _restoreIsDelayed;

        public bool CanUse { get; private set; } = true;
        
        public Stamina(float currentValue, float maxValue, int staminaRestoreDelay) : base(currentValue, maxValue)
        {
            _staminaRestoreDelay = staminaRestoreDelay;
        }

        public Stamina(float currentValue, float maxValue, int staminaRestoreDelay, ValueBar bar) 
            : base(currentValue, maxValue, bar)
        {
            _staminaRestoreDelay = staminaRestoreDelay;
        }

        public bool CanRestore() => CurrentValue < MaxValue && !_restoreIsDelayed;
        
        public override void Increase(float value)
        {
            if (_restoreIsDelayed) return;
            if (CurrentValue > MaxValue / 4) CanUse = true;
            base.Increase(value);
        }

        public override void Decrease(float value)
        {
            if (!CanUse) return;

            base.Decrease(value);

            if (CurrentValue > MinValue) return;

            CanUse = false;
            _restoreIsDelayed = true;

            Task.Delay(_staminaRestoreDelay).ContinueWith(_ =>
            {
                CanUse = true;
                _restoreIsDelayed = false;
            });
        }
    }
}