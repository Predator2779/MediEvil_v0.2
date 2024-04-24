using System.Threading.Tasks;
using Character.Classes;
using Character.ComponentContainer;
using Input;
using UnityEngine;

namespace Character.CharacterControllers.Inputs
{
    public sealed class InputController : Controller
    {
        private Warrior _warrior;
        private Thrower _thrower;
        private InputHandler _inputHandler;

        private bool _canCombo;
        private int _countComboClicks;

        public InputController(PersonContainer container) : base(container)
        {
            _warrior = new Warrior(container);
            _thrower = new Thrower(container);
        }

        public override void Initialize()
        {
            base.Initialize();
            _warrior.Initialize();
            _thrower.Initialize();
            _inputHandler = new InputHandler();
        }

        private void CheckConditions()
        {
            if (IsFall())
            {
                Fall();
                return;
            }

            if (IsJump())
            {
                Jump();
                return;
            }

            if (IsSlide())
            {
                Slide();
                return;
            }

            if (IsRoll())
            {
                Roll();
                return;
            }
            
            if (IsInteract()) Interact();
            
            if (IsThrowWeapon()) ThrowControl();
            
            if (IsDefense())
            {
                Defense();
                return;
            }

            if (_countComboClicks > 0)
            {
                if (CanEnterState())
                {
                    _countComboClicks = 0;
                    ComboAttack();
                }

                return;
            }

            if (IsAttack())
            {
                Attack();
                return;
            }

            if (IsRun())
            {
                Run();
                return;
            }

            if (IsWalk())
            {
                Walk();
                return;
            }

            Idle();
        }

        public override void Execute()
        {
            base.Execute();
            TrackCombo();
            CheckConditions();
            SetTempDirection(GetDirection());
        }

        private Vector2 GetDirection() => new Vector2(
            _inputHandler.GetHorizontalAxis(),
            _inputHandler.GetVerticalAxis());
        
        private bool IsInteract() => _inputHandler.GetInteract();
        private bool IsThrowWeapon() => _inputHandler.GetThrowBtn();
        private bool IsWalk() => _inputHandler.GetHorizontalAxis() != 0 && _person.Container.Movement.IsGrounded();
        private bool IsRun() => IsWalk() && _inputHandler.GetShiftBtn() && _person.Container.Stamina.CanUse;
        private bool IsFall() => !_person.Container.Movement.IsGrounded() && _person.Container.Movement.IsFall();

        private bool IsJump() => _inputHandler.GetVerticalAxis() > 0 &&
                                 _person.Container.Stamina.CanUse &&
                                 !_person.Container.Movement.IsFall() &&
                                 _person.Container.Movement.IsGrounded();

        // добавить атаку в прыжке (для варриора)
        private bool IsSlide() => _inputHandler.GetVerticalAxis() < 0 &&
                                  _person.Container.Movement.IsGrounded() &&
                                  _person.Container.Movement.CanSlide();

        private bool IsRoll() => _inputHandler.GetSpaceBtn() &&
                                 _person.Container.Stamina.CanUse &&
                                 !_person.Container.Movement.IsFall() &&
                                 _person.Container.Movement.IsGrounded();

        private bool IsAttack() => _inputHandler.GetLMB();
        private bool IsDefense() => _inputHandler.GetRMB();

        private void TrackCombo()
        {
            if (IsAttack() && _canCombo) _countComboClicks++;
        }
        
        private void Attack()
        {
            SubscribeEndedAttack();
            _warrior.Attack();
        }

        private void ComboAttack()
        {
            SubscribeEndedAttack();
            _warrior.ComboAttack();
        }

        private void SubscribeEndedAttack() => _warrior.OnEndedAttack += ResetCombo;

        private void ResetCombo()
        {
            _warrior.OnEndedAttack -= ResetCombo;
            _canCombo = true;

            Task.Delay(_warrior.Container.Config.ComboInterval)
                .ContinueWith(_ => { _canCombo = false; });
        }

        private void Interact() => _person.Container.ItemHandler.HandleItem();
        private void Fall() => _person.Fall();
        private void Jump() => _person.Jump();
        private void Roll() => _person.Roll();
        private void Run() => _person.Run();
        private void Walk() => _person.Walk();
        private void Idle() => _person.Idle();
        private bool CanEnterState() => _person.Container.StateMachine.CurrentState.IsCompleted;
        
        private void ThrowControl()
        {
            if (_container.WeaponHandler.CurrentWeapon != null) ThrowWeaponTwisted();
            else if (_container.WeaponHandler.DropedWeapon != null) PullWeapon();
        }
        
        private void ThrowWeaponTwisted() => _thrower.ThrowWeaponTwisted();
        private void PullWeapon() => _thrower.PullWeapon();
        private void Defense() => _warrior.Defense();
        private void Slide() => _warrior.CombatSlide();

        private void SetTempDirection(Vector2 input)
        {
            _person.Container.Movement.Direction = input;

            if (Mathf.Abs(input.x) > 0)
                _person.Container.Movement.TempDirection = new Vector2(input.x, 0);
        }
    }
}