using Character.ValueStorages;
using Character.ValueStorages.Bars;

namespace Economy
{
    public class SoulWallet : ValueStorage
    {
        protected SoulWallet(float currentValue) : base(currentValue)
        {
        }
        
        public SoulWallet(float currentValue, float maxValue) : base(currentValue, maxValue)
        {
        }
     
        public SoulWallet(float currentValue, CountBar bar) : base(currentValue, bar)
        {
            ChangeBar();
        }

        public SoulWallet(float currentValue, float maxValue, CountBar bar) : base(currentValue, maxValue, bar)
        {
        }

        protected override void ChangeBar()
        {
            if (Bar != null) Bar.SetCurrentValue(CurrentValue);
        }
    }
}