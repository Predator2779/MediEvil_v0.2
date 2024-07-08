using Character.CharacterControllers.Inputs;
using Character.ComponentContainer;
using Cinemachine;
using Economy;
using Economy.Items;
using Economy.Items.Souls;
using UI;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Creators
{
    public class PlayerCreator : AbstractUnitCreator
    {
        [SerializeField] protected Camera _cameraPrefab;
        [SerializeField] protected ValueBarContainer _barContainer;
        [SerializeField] protected Image _weaponUi;
        [SerializeField] private CinemachineVirtualCamera _cinemachine;
        [SerializeField] private SoulItem _dropSoulsPrefab;

        protected override void InstantiateUnitComponents()
        {
            CreateUnit(_unitPrefabBase);
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
            personContainer.IsPlayer = true;
            personContainer.SoulBar = _barContainer.SoulBar;
            personContainer.HealthBar = _barContainer.HealthBar;
            personContainer.StaminaBar = _barContainer.StaminaBar;
            personContainer.ManaBar = _barContainer.ManaBar;
            personContainer.WeaponHandler.WeaponUi = _weaponUi;
            
            InitDropSouls(personContainer);
        }
        
        private void InitDropSouls(PersonContainer personContainer)
        {
            var dropSoulsUnit = Instantiate(_dropSoulsPrefab, transform.position, Quaternion.identity);
            dropSoulsUnit.gameObject.SetActive(false);
            personContainer.SoulsHandler = new SoulsHandler(personContainer, dropSoulsUnit);
        }
    }
}