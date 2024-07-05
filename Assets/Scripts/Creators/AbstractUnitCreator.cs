using Character.ComponentContainer;
using Character.Configs;
using Character.Interaction;
using Character.Movement;
using Damageables.Weapons;
using UnityEngine;
using VFX;

namespace Creators
{
    public abstract class AbstractUnitCreator : AbstractCreator
    {
        [SerializeField] protected GameObject _weaponPrefab;
        [SerializeField] protected CharacterConfig _config;

        protected PersonContainer _container;

        protected override void StartCreator()
        {
            InstantiateUnitComponents();
            SetSpawnPoint();
            SetController();
            SetWeapon();
            Initialize();
            DisableCreator();
        }

        protected abstract void SetController();
        private void SetSpawnPoint() => _container.StartSpawnPoint = transform.position;
        protected override void Initialize() => _container.Initialize();
        protected void CreateContainer() => _container = _unit.AddComponent<PersonContainer>();

        protected virtual void SetFields(PersonContainer personContainer)
        {
            personContainer.Config ??= _config;
            personContainer.Movement ??= _unit.AddComponent<CharacterMovement>();
            personContainer.Animator ??= _unit.GetComponent<Animator>();
            personContainer.ItemHandler ??= _unit.GetComponentInChildren<ItemHandler>();
            personContainer.WeaponHandler ??= _unit.GetComponentInChildren<WeaponHandler>();
            personContainer.DustEffectPlayer ??= _unit.GetComponentInChildren<DustEffectPlayer>();
        }

        private void SetWeapon()
        {
            if (_weaponPrefab == null) return;

            var weaponHandler = _container.WeaponHandler;

            var weapon = Instantiate(
                _weaponPrefab,
                weaponHandler.transform.position,
                Quaternion.identity);

            weaponHandler.EquipWeapon(weapon.GetComponent<Weapon>());
        }
    }
}