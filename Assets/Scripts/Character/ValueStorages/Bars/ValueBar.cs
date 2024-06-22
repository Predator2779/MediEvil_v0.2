using UnityEngine;
using UnityEngine.UI;

namespace Character.ValueStorages.Bars
{
    public class ValueBar : ViewBar
    {
        [SerializeField] protected Slider slider;
        [SerializeField] protected Image fill;
      
        public override void SetCurrentValue(float value)
        {
            slider.value = value;
        }
    }
}