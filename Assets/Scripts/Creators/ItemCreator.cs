using Economy.Items;
using UnityEngine;

namespace Creators
{
    public class ItemCreator : AbstractCreator
    {
        [SerializeField] private ItemData _data;
        [SerializeField] private int _itemCount;

        private Item _item;
        
        protected override void InstantiateUnitComponents() => CreateUnit();
        
        protected override void Initialize()
        {
            _item = GetItemComponent();
            if (_item == null) return;
            SetData();
            SetCount();
        }

        private Item GetItemComponent()
        {
            var item = _unit.GetComponent<Item>();
            if (item == null) item = _unit.GetComponentInChildren<Item>();
            return item;
        }
        
        private void SetData()
        {
            if (_data != null) _item.ItemData = _data;
        }
        
        private void SetCount()
        {
            if (_itemCount > 0) _item.Count = _itemCount;
        }
    }
}