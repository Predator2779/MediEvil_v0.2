using System;
using System.Collections.Generic;
using System.Linq;
using Economy.Items;
using UnityEngine;

namespace Economy
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private List<ItemSet> _listItems = new List<ItemSet>();
        [SerializeField] private int _size;

        public void AddItems(Item item, int count)
        {
            if (HasItems(item.Data.name, 1)) AddItem(item.Data.name, count);
            else if (HasFreeSpace()) CreateAndAdd(item, count);
        }

        public bool TryGetItems(string name, int count, out ItemSet set)
        {
            set = null;

            if (HasItems(name, count))
            {
                set = GetItemSet(name, count);
                RemoveItem(name, count);
                return true;
            }
            
            if (HasItems(name, 1))
            {
                set = GetItemSet(name);
                RemoveItem(name, set.Count);
                return true;
            }

            return false;
        }

        private void AddItem(string name, int count) => GetItemSet(name).AddItems(count);
        private void CreateAndAdd(Item item, int count) => _listItems.Add(new ItemSet(item, count));

        private void RemoveItem(string name, int count)
        {
            var set = GetItemSet(name);
            set.RemoveItems(count);
            if (set.Count <= 0) _listItems.Remove(set);
        }

        private ItemSet GetItemSet(string name) => _listItems.FirstOrDefault(set => set.Item.Data.Name == name);

        private ItemSet GetItemSet(string name, int count) =>
            _listItems.FirstOrDefault(set => set.Item.Data.Name == name && set.Count == count);

        private bool HasItems(string name, int count) => GetItemSet(name, count) != null;
        private bool HasFreeSpace() => _listItems.Count < _size;
    }
}