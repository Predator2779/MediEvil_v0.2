using System;
using UnityEngine;

namespace Economy
{
    [CreateAssetMenu(menuName = "Inventory/Item", fileName = "New Item", order = 0)]
    [Serializable] public class ItemData : ScriptableObject
    {
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public int Price { get; private set; }
        [field: SerializeField] public bool IsOneTime { get; private set; }
    }
}