using Economy.Items;
using UnityEngine;

namespace Economy
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class InventoryHandler : MonoBehaviour
    {
        [field: SerializeField] public Inventory Inventory { get; set; }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Item item)) PickUp(item);
        }

        private void PickUp(Item item)
        {
            Inventory.AddItems(item, item.Count);
            item.PickUp();
        }
    }
}