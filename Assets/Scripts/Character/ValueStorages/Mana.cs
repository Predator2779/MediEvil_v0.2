using Character.ComponentContainer;
using Character.ValueStorages.Bars;

namespace Character.ValueStorages
{
    public class Mana : Stamina
    {
        public Mana(float currentValue, float maxValue, int manaRestoreDelay) 
            : base(currentValue, maxValue, manaRestoreDelay) 
        {
        }

        public Mana(float currentValue, float maxValue, int manaRestoreDelay, ValueBar bar) 
            : base(currentValue, maxValue, manaRestoreDelay, bar)
        {
        }
    }
}