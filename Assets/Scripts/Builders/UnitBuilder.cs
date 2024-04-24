// using Character;
// using Character.CharacterControllers.AI;
// using Character.CharacterControllers.Inputs;
// using Character.Classes;
// using Character.ComponentContainer;
// using UI;
// using UnityEngine;
//
// namespace Builders
// {
//     public class UnitBuilder
//     {
//         private GameObject _unit;
//         private PersonContainer _personContainer;
//         private readonly CharacterConfig _config;
//         private readonly ValueBarContainer _barContainer;
//         private readonly ScopeCoverage _scopeCoverage;
//
//         public UnitBuilder(
//             GameObject baseObject,
//             CharacterConfig config,
//             ValueBarContainer barContainer)
//         {
//             _unit = baseObject;
//             _config = config;
//             _barContainer = barContainer;
//             _personContainer = _unit.AddComponent<PersonContainer>();
//         }
//         
//         public UnitBuilder(
//             GameObject baseObject,
//             CharacterConfig config,
//             ValueBarContainer barContainer,
//             ScopeCoverage scopeCoverage)
//         {
//             _unit = baseObject;
//             _config = config;
//             _barContainer = barContainer;
//             _scopeCoverage = scopeCoverage;
//             _personContainer = _unit.AddComponent<PersonContainer>();
//         }
//
//         public UnitBuilder BuildPerson()
//         {
//             return this;
//         }
//
//         public UnitBuilder BuildWarriorAI()
//         {
//             _controller.SetWarrior(new Warrior(_personContainer, null));
//             return this;
//         }
//
//         public UnitBuilder BuildThrower()
//         {
//             return this;
//         }
//
//         public UnitBuilder BuildMage()
//         {
//             return this;
//         }
//
//         public GameObject GetResult()
//         {
//             SetFields(_personContainer);
//             return _unit;
//         }
//
//         public void SetInputController()
//         {
//             _personContainer.Controller = new InputController(new Person(_personContainer));
//         } 
//         
//         public void SetWarriorAIController()
//         {
//             _personContainer.Controller = new WarriorAI(new Person(_personContainer), _scopeCoverage);
//         }
//
//         private void SetFields(PersonContainer personContainer)
//         {
//             personContainer.Config = _config;
//
//             personContainer.HealthBar = _barContainer.HealthBar;
//             personContainer.StaminaBar = _barContainer.StaminaBar;
//             personContainer.ManaBar = _barContainer.ManaBar;
//         }
//     }
// }