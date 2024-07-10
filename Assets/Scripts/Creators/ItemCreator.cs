using Economy.Items;
using UnityEngine;

namespace Creators
{
    public class ItemCreator : AbstractCreator
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private int _count;
        [SerializeField] private int _price;

        protected override void InstantiateUnitComponents() => CreateUnit(_unitPrefabBase);

        protected override void Initialize()
        {
            var item = GetOrCreateItemComponent();
            item.Icon = _icon;
            item.Name = _name;
            item.Description = _description;
            item.Price = _price;
            item.Count = _count;
        }

        private Item GetOrCreateItemComponent() => 
            _unitPrefabBase.TryGetComponent(out Item item) ? item : _unitPrefabBase.AddComponent<Item>();
    }
}