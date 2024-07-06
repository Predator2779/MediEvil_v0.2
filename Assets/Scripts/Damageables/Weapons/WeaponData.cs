using Economy.Items;
using UnityEngine;

namespace Damageables.Weapons
{
    [CreateAssetMenu(menuName = "Configs/Weapons", fileName = "New WeaponConfig", order = 0)]
    public class WeaponData : ItemData
    {
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float AttackRadius { get; private set; }
    }
}