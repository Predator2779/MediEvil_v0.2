using Character.CharacterControllers.Inputs;
using Character.ComponentContainer;
using Other.Follow;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Builders.Creators
{
    public class PlayerCreator : AbstractUnitCreator
    {
        [SerializeField] protected Following _cameraPrefab;
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
            var camera = Instantiate(
                _cameraPrefab,
                transform.position + _cameraPrefab.transform.position,
                Quaternion.identity, _path);

            camera.Target = _unit.transform;
        }

        protected override void SetFields(PersonContainer personContainer)
        {
            base.SetFields(personContainer);

            personContainer.IsPlayer = true;
            personContainer.HealthBar = _barContainer.HealthBar;
            personContainer.StaminaBar = _barContainer.StaminaBar;
            personContainer.ManaBar = _barContainer.ManaBar;
            personContainer.WeaponHandler.WeaponUi = _weaponUi;
        }
    }
}