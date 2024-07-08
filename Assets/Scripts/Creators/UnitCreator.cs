using System;
using Cam_Settings.Follow;
using Character.CharacterControllers.AI;
using Character.ComponentContainer;
using Global;
using UI;
using UnityEngine;

namespace Creators
{
    public class UnitCreator : AbstractUnitCreator
    {
        [SerializeField] private TypeController _controller;
        [SerializeField] private ValueBarContainer _prefabBarContainer;

        private ScopeCoverage _scopeCoverage;

        protected override void InstantiateUnitComponents()
        {
            _scopeCoverage = GetComponent<ScopeCoverage>();

            CreateUnit(_unitPrefabBase);
            CreateContainer();
            SetFields(_container);
            SendSpawnMessage();
        }

        protected override void SetController()
        {
            switch (_controller)
            {
                case TypeController.PersecutorAI:
                    _container.Controller = new PersecutorAI(_container, _scopeCoverage);
                    break;
                case TypeController.WarriorAI:
                    _container.Controller = new WarriorAI(_container, _scopeCoverage);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override void SetFields(PersonContainer personContainer)
        {
            base.SetFields(personContainer);

            var barContainer = _unitPrefabBase.GetComponentInChildren<ValueBarContainer>();

            if (barContainer == null)
            {
                barContainer = Instantiate(
                    _prefabBarContainer,
                    _unit.transform.position,
                    Quaternion.identity,
                    FindOrCreatePath("EnemyBarContainers"));
            }

            barContainer.gameObject.GetComponent<Following>().Target = _unit.transform;
            personContainer.HealthBar = barContainer.HealthBar;
            personContainer.StaminaBar = barContainer.StaminaBar;
            personContainer.ManaBar = barContainer.ManaBar;
        }

        private void SendSpawnMessage() => EventBus.OnUnitSpawned?.Invoke(_container);
        
        private enum TypeController
        {
            PersecutorAI,
            WarriorAI
        }
    }
}