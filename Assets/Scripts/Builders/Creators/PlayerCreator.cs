using Character.CharacterControllers.Inputs;
using Character.ComponentContainer;
using Cinemachine;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Builders.Creators
{
    public class PlayerCreator : AbstractUnitCreator
    {
        [SerializeField] protected Camera _cameraPrefab;
        [SerializeField] private CinemachineVirtualCamera _cinemachine;
        [SerializeField] protected ValueBarContainer _barContainer;
        [SerializeField] protected Image _weaponUi;

        protected override void InstantiateUnitComponents()
        {
            CreateUnit();
            CreateContainer();
            CreateCamera();
        }
        
        protected override void SetController() => _container.Controller = new InputController(_container);
        
        private void CreateCamera()
        {
            Instantiate(_cameraPrefab, transform.position + _cameraPrefab.transform.position, Quaternion.identity, _path);
            _cinemachine.Follow = _unit.transform;
            _cinemachine.LookAt = _unit.transform;
        }

        protected override void SetFields(PersonContainer personContainer)
        {
            base.SetFields(personContainer);

            personContainer.IsPlayer = true;
            personContainer.SoulBar = _barContainer.SoulBar;
            personContainer.HealthBar = _barContainer.HealthBar;
            personContainer.StaminaBar = _barContainer.StaminaBar;
            personContainer.ManaBar = _barContainer.ManaBar;
            personContainer.WeaponHandler.WeaponUi = _weaponUi;
        }
    }
}