using System;
using Character.CharacterControllers.AI;
using Character.ComponentContainer;
using UI;
using UnityEngine;

namespace Builders.Creators
{
    public class UnitCreator : AbstractUnitCreator
    {
        [SerializeField] private TypeController _controller;
        [SerializeField] private ValueBarContainer _prefabBarContainer;

        private ScopeCoverage _scopeCoverage;

        protected override void InstantiateUnitComponents()
        {
            _scopeCoverage = GetComponent<ScopeCoverage>();
            
            CreateUnit();
            CreateContainer();
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
                    _unit.transform);
            }

            personContainer.HealthBar = barContainer.HealthBar;
            personContainer.StaminaBar = barContainer.StaminaBar;
            personContainer.ManaBar = barContainer.ManaBar;
        }

        private enum TypeController
        {
            PersecutorAI,
            WarriorAI
        }
    }
}