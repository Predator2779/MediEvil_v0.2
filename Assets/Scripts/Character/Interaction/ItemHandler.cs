using System.Collections.Generic;
using Damageables.Weapons;
using Environments.Items;
using Global;
using UnityEngine;

namespace Character.Interaction
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class ItemHandler : MonoBehaviour
    {
        public GlobalConstants.WeaponCallback OnWeaponPickedUp;
        
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
        
        public void HandleItem()
        {
            if (_selectedItems.Count <= 0) return;
            
            var item = GetItem();
            if (item != null && item.TryGetComponent(out Weapon weapon)) HandleWeapon(weapon);
        }

        private void HandleWeapon(Weapon weapon)
        {
            _selectedItems.Remove(weapon);
            OnWeaponPickedUp?.Invoke(weapon);
        }
        
        public float GetDetectionRadius() => _collider.radius;
        private Item GetItem() => _selectedItems[0];
    }
}