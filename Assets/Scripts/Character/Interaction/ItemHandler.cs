using System.Collections.Generic;
using Damageables.Weapons;
using Economy;
using Economy.Items;
using Global;
using UnityEngine;

namespace Character.Interaction
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class ItemHandler : MonoBehaviour
    {
        public GlobalConstants.WeaponCallback OnWeaponPickedUp;
        public GlobalConstants.ItemCallback OnItemPickedUp;
        
        [SerializeField] private List<Item> _selectedItems = new List<Item>();
        private CircleCollider2D _collider;

        private void Awake() => _collider = GetComponent<CircleCollider2D>();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Item item) && !_selectedItems.Contains(item))
                _selectedItems.Add(item);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Item item) && _selectedItems.Contains(item))
                _selectedItems.Remove(item);
        }

        public float GetDetectionRadius() => _collider.radius;

        public void Handle()
        {
            if (_selectedItems.Count <= 0) return;
            
            var item = GetItem();
            if (item == null) return;
            
            if (item.TryGetComponent(out Weapon weapon)) HandleWeapon(weapon);
            else HandleItem(item);
        }

        private void HandleWeapon(Weapon weapon)
        {
            _selectedItems.Remove(weapon);
            OnWeaponPickedUp?.Invoke(weapon);
        }

        private void HandleItem(Item item)
        {
            OnItemPickedUp?.Invoke(item);
            item.PickUp();
        }

        private Item GetItem() => _selectedItems[0];
    }
}