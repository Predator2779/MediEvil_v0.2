using UnityEngine;

namespace Economy.Items
{
    public abstract class Item : MonoBehaviour
    {
        [SerializeField] [Min(1)] private int _count = 1;
        
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public string Name { get; set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public int Price { get; private set; }
        public int Count { get => _count; set => _count = value; }

        public abstract void PickUp();
        public abstract void Put();
    }
}