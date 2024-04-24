using Character.Classes;
using Character.ComponentContainer;
using UnityEngine;

namespace Character.CharacterControllers.AI
{
    public class WarriorAI : PersecutorAI
    {
        private readonly Warrior _warrior;
        private bool _staminaRestore;

        public WarriorAI(PersonContainer container, ScopeCoverage scopeCoverage) : base(container, scopeCoverage)
        {
            _warrior = new Warrior(container);
        }

        public override void Initialize()
        {
            base.Initialize();
            _warrior?.Initialize();
        }
        
        public override void Execute()
        {
            _container.StateMachine.Execute();

            SetTempDirection();
            StaminaControl();
            
            if (!HasTarget())
            {
                Idle();
                return;
            }

            _warrior?.Container.Movement.LookTo(_target.transform);
            
            if (_staminaRestore)
            {
                if (CanStay())
                {
                    Idle();
                    return;
                }
                
                WalkFollow();
                return;
            }

            if (TargetIsNear())
            {
                if (CanAttack()) Attack();
                else Idle();
                return;
            }

            if (CanWalkFollow())
            {
                WalkFollow();
                return;
            }  
            
            if (CanRunFollow())
            {
                RunFollow();
            }
        }

        private void StaminaControl()
        {
            if (_person?.Container.Stamina.GetPercentageRation() > 30) _staminaRestore = false; // написать конфиг для ИИ
            if (_person?.Container.Stamina.GetPercentageRation() <= 0) _staminaRestore = true;
        }
        
        private void Attack()
        {
            if (Random.Range(0, _person.Container.Config.ComboChanceAI) == 0) _warrior?.Attack(); 
            else _warrior?.ComboAttack();
        }

        private bool TargetIsNear() => GetTargetDistance() <= _stayDistance;
        private bool CanAttack() => _warrior.Container.WeaponHandler.CurrentWeapon != null;
    }
}