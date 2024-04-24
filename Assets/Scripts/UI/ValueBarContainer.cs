using Character.ValueStorages.Bars;
using UnityEngine;

namespace UI
{
    public class ValueBarContainer : MonoBehaviour
    {
        [field: SerializeField] public ValueBar HealthBar { get; set; }
        [field: SerializeField] public ValueBar StaminaBar { get; set; }
        [field: SerializeField] public ValueBar ManaBar { get; set; }
    }
}