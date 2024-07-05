using Economy.Items;
using UnityEngine;

namespace Builders.Creators
{
    public class ItemCreator : AbstractCreator
    {
        [SerializeField] private ItemData _data;
        [SerializeField] private int _itemCount;

        private Item _item;
        
        protected override void InstantiateUnitComponents() => CreateUnit();
        
        protected override void Initialize()
        {
            _item = GetComponent<Item>();
            if (_item == null) return;
            SetData();
            SetCount();
        }
        
        private void SetData()
        {
            if (_data != null) _item.ItemData = _data;
        }
        
        private void SetCount()
        {
            if (_itemCount != 0) _item.Count = _itemCount;
        }
    }
}