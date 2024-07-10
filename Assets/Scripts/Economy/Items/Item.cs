using UnityEngine;

namespace Economy.Items
{
    public abstract class Item : MonoBehaviour
    {
        [SerializeField] [Min(1)] private int _count = 1;
        
        [field: SerializeField] public Sprite Icon { get; set; }
        [field: SerializeField] public string Name { get; set; }
        [field: SerializeField] public string Description { get; set; }
        [field: SerializeField] public int Price { get; set; }
        public int Count { get => _count; set => _count = value; }

        public abstract void PickUp();
        public abstract void Put();
    }
}