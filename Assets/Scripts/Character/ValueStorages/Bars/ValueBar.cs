using UnityEngine;
using UnityEngine.UI;

namespace Character.ValueStorages.Bars
{
    public class ValueBar : MonoBehaviour
    {
        [SerializeField] protected Slider slider;
        [SerializeField] protected Image fill;
      
        public virtual void SetCurrentValue(float value)
        {
            slider.value = value;
        }
    }
}