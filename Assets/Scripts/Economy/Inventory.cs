using System;
using System.Collections.Generic;
using System.Linq;
using Economy.Items;
using UnityEngine;

namespace Economy
{
    [Serializable]
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private List<ItemSet> _listItems = new List<ItemSet>();
        [SerializeField] [Min(10)] private int _size = 10;

        public bool HasItems(string name, int count) => GetItemSet(name, count) != null;

        public void AddItems(Item item)
        {
            if (HasItems(item.ItemData.Name, 1)) AddItem(item.ItemData.Name, item.Count);
            else if (HasFreeSpace()) CreateAndAdd(item, item.Count);
        }

        public bool TryGetItems(string name, int count, out ItemSet set)
        {
            set = null;

            // если есть предмет с количеством - вернуть
            if (HasItems(name, count))
            {
                set = GetItemSet(name, count);
                RemoveItem(name, count);
                return true;
            }

            // если нет - вернуть сколько есть
            if (TryGetItems(name, out set))
                return true;

            return false;
        }

        public bool TryGetItems(string name, out ItemSet set)
        {
            set = null;

            // вернуть сколько есть
            if (HasItems(name, 0))
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

        private void RemoveItemAll(string name) => _listItems.Remove(GetItemSet(name));
        private ItemSet GetItemSet(string name) => _listItems.FirstOrDefault(set => set.Item.ItemData.Name == name);

        private ItemSet GetItemSet(string name, int count) =>
            _listItems.FirstOrDefault(set => set.Item.ItemData.Name == name && set.Count >= count);

        private bool HasFreeSpace() => _listItems.Count < _size;
    }
}