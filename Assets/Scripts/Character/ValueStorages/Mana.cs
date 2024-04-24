using Character.ComponentContainer;
using Character.ValueStorages.Bars;

namespace Character.ValueStorages
{
    public class Mana : Stamina
    {
        public Mana(PersonContainer personContainer, float currentValue, float maxValue) : base(personContainer, currentValue, maxValue) 
        {
        }

        public Mana(PersonContainer personContainer, float currentValue, float maxValue, ValueBar bar) : base(personContainer, currentValue, maxValue, bar)
        {
        }
    }
}