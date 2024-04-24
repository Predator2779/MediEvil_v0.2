using UnityEngine;
using UnityEngine.UI;

namespace Damageables.Weapons
{
    public class WeaponHandler : MonoBehaviour
    {
        [field: SerializeField] public Weapon CurrentWeapon { get; set; }
        [field: SerializeField] public Image WeaponUi { get; set; }
        public Weapon DropedWeapon { get; set; }

        public void EquipWeapon(Weapon weapon)
        {
            DropWeapon();

            CurrentWeapon = weapon;
            CurrentWeapon.PickUp();
            SetPosition(transform.position);
            SetWeaponParent(transform);
            SetSprite(CurrentWeapon.Data.Icon);
        }

        public void DropWeapon()
        {
            if (CurrentWeapon == null) return;

            SetWeaponParent(null);
            EnabledSprite(false);
            DropedWeapon = CurrentWeapon;
            CurrentWeapon.Put();
            CurrentWeapon = null;
        }

        private void SetSprite(Sprite sprite)
        {
            if (WeaponUi == null) return;
            
            EnabledSprite(true);
            WeaponUi.sprite = sprite;
        }

        private void EnabledSprite(bool value) => WeaponUi.enabled = value;
        private void SetWeaponParent(Transform parent) => CurrentWeapon.transform.parent = parent;
        private void SetPosition(Vector2 position) => CurrentWeapon.transform.position = position;
    }
}