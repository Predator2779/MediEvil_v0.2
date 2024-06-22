using TMPro;
using UnityEngine;

namespace Character.ValueStorages.Bars
{
    public class CountBar : ViewBar
    {
        [SerializeField] private TMP_Text _countText;
        
        public override void SetCurrentValue(float value)
        {
            _countText.text = value.ToString();
        }
    }
}