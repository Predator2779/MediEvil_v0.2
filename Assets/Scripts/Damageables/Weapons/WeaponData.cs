using UnityEngine;

namespace Damageables.Weapons
{
    [CreateAssetMenu(menuName = "Configs/Weapons", fileName = "New WeaponConfig", order = 0)]
    public class WeaponData : ScriptableObject
    {
        [Header("About")] [Space]
        public Sprite Icon;
        public string Name;

        [Space] [Header("Parameters")]
        public float Damage;
        public float AttackRadius;
    }
}