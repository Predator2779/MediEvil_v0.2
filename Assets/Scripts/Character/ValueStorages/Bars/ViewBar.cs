using UnityEngine;

namespace Character.ValueStorages.Bars
{
    public abstract class ViewBar : MonoBehaviour
    {
        public abstract void SetCurrentValue(float value);
    }
}