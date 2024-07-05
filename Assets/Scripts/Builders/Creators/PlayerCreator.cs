using Character.CharacterControllers.Inputs;
using Character.ComponentContainer;
using Cinemachine;
using Economy;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Builders.Creators
{
    public class PlayerCreator : AbstractUnitCreator
    {
        [SerializeField] protected Camera _cameraPrefab;
        [SerializeField] protected ValueBarContainer _barContainer;
        [SerializeField] protected Image _weaponUi;
        [SerializeField] private CinemachineVirtualCamera _cinemachine;

        protected override void InstantiateUnitComponents()
        {
            CreateUnit();
            CreateContainer();
            SetFields(_container);
            CreateCamera();
        }

        protected override void SetController() => _container.Controller = new InputController(_container);

        private void CreateCamera()
        {
            Instantiate(_cameraPrefab, transform.position + _cameraPrefab.transform.position, Quaternion.identity,
                FindOrCreatePath(_path));
            _cinemachine.Follow = _unit.transform;
            _cinemachine.LookAt = _unit.transform;
        }

        protected override void SetFields(PersonContainer personContainer)
        {
            base.SetFields(personContainer);
            SetPlayerFields(personContainer);
        }

        private void SetPlayerFields(PersonContainer personContainer)
        {
            personContainer.Inventory ??= _unit.AddComponent<Inventory>();
            
            personContainer.IsPlayer = true;
            personContainer.SoulBar = _barContainer.SoulBar;
            personContainer.HealthBar = _barContainer.HealthBar;
            personContainer.StaminaBar = _barContainer.StaminaBar;
            personContainer.ManaBar = _barContainer.ManaBar;
            personContainer.WeaponHandler.WeaponUi = _weaponUi;
        }
    }
}