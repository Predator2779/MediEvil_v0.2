using UnityEngine;

namespace Economy.Items
{
    public abstract class Item : MonoBehaviour
    {
        [SerializeField] [Min(1)] private int _count = 1;

        [field: SerializeField] public ItemData ItemData { get; set; }

        public int Count { get => _count; set => _count = value; }

        public abstract void PickUp();
        public abstract void Put();
    }
}