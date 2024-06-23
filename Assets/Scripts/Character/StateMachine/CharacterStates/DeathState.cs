using Character.Classes;
using Global;
using UnityEngine;

namespace Character.StateMachine.CharacterStates
{
    public class DeathState : CharacterState
    {
        private readonly Person _person;
        private delegate void Message();
        private event Message _animComplete;

        public DeathState(Person person) : base(person.Container)
        {
            Animation = "death";
            _person = person;
        }

        public override void Enter()
        {
            base.Enter();
            _animComplete += Die;
            _person.Container.IsDeath = true;
        }

        public override void Execute()
        {
            IsCompleted = !_person.Container.IsDeath;
            if (AnimationCompleted()) _animComplete?.Invoke();
        }

        public override void FixedExecute()
        {
        }

        private void Die()
        {
            _animComplete -= Die;
            PersonContainer.Movement.SetBodyType(RigidbodyType2D.Static);
            PersonContainer.DustEffectPlayer.PlayStep();

            if (PersonContainer.IsPlayer) EventBus.OnPlayerDied?.Invoke(PersonContainer);
            else EventBus.OnUnitDied?.Invoke(PersonContainer);
        }
    }
}