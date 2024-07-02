using UnityEngine;

namespace Economy.Items
{
    public abstract class Item : MonoBehaviour
    {
        [SerializeField] [Min(0)] private int _count;

        [field: SerializeField] public ItemData ItemData { get; private set; }

        public int Count
        {
            get => ItemData.IsOneTime ? 1 : _count;
            set => _count = ItemData.IsOneTime ? 1 : value;
        }
        
        public abstract void PickUp();
        public abstract void Put();
    }
}