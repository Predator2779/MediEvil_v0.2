using UnityEngine;

namespace Economy.Items
{
    public abstract class Item : MonoBehaviour
    {
        [SerializeField] [Min(0)] private int _count;

        [field: SerializeField] public ItemData Data { get; private set; }

        public int Count
        {
            get => Data.IsOneTime ? 1 : _count;
            set => _count = Data.IsOneTime ? 1 : value;
        }

        public abstract void PickUp();
    }
}