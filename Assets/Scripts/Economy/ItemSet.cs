using System;
using Economy.Items;
using UnityEngine;

namespace Economy
{
    [Serializable] public class ItemSet
    {
        [field: SerializeField] public Item Item { get; private set; }
        [field: SerializeField] public int Count { get; private set; }

        public ItemSet(Item item, int count = 0)
        {
            Item = item;
            Count = count;
        }

        public void AddItems(int value) => Count += IsValidValue(value) ? value : 0;
        public void RemoveItems(int value) => Count += IsValidValue(value) && CanRemoveValue(value) ? -value : 0;
        private bool IsValidValue(int value) => value > 0;
        private bool CanRemoveValue(int value) => value >= Count;
    }
}