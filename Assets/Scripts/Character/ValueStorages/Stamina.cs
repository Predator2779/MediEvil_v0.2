using System.Threading.Tasks;
using Character.ComponentContainer;
using Character.ValueStorages.Bars;

namespace Character.ValueStorages
{
    public class Stamina : ValueStorage
    {
        public bool CanUse { get; private set; }  = true;
        private PersonContainer PersonContainer { get; }

        private bool _restoreIsDelayed;

        public Stamina(PersonContainer personContainer, float currentValue, float maxValue) : base(currentValue, maxValue) 
        {
            PersonContainer = personContainer;
        }

        public Stamina(PersonContainer personContainer, float currentValue, float maxValue, ValueBar bar) : base(currentValue, maxValue, bar)
        {
            PersonContainer = personContainer;
        }

        public override void Increase(float value)
        {
            if (_restoreIsDelayed) return;
            if (CurrentValue > MaxValue / 4)  CanUse = true;
            base.Increase(value);
        }

        public override void Decrease(float value)
        {
            if (!CanUse) return;
            
            base.Decrease(value);

            if (CurrentValue > MinValue) return;
            
            CanUse = false;
            _restoreIsDelayed = true;
                 
            Task.Delay(PersonContainer.Config.StaminaRestoreDelay).ContinueWith(_ => { CanUse = true; _restoreIsDelayed = false; });
        }

        public bool CanRestore() => CurrentValue < MaxValue && !_restoreIsDelayed;
    }
}