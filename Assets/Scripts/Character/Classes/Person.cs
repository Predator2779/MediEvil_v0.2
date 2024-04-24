using Character.ComponentContainer;
using Character.StateMachine;
using Character.StateMachine.StateSets;

namespace Character.Classes
{
    public class Person
    {
        public PersonContainer Container { get; }
        private PersonStateSet _personStateSet;

        public Person(PersonContainer container)
        {
            Container = container;
            _personStateSet = new PersonStateSet(this);
            Container.StateMachine = new PersonStateMachine(_personStateSet.DefaultState);
        }

        public virtual void Initialize() => Subscribe();

        private void Subscribe()
        {
            Container.Health.Falldown += FallDown;
            Container.Health.Die += Die;
        }

        public void Describe()
        {
            Container.Health.Falldown -= FallDown;
            Container.Health.Die -= Die;
        }

        public void Idle() => Container.StateMachine.ChangeState(_personStateSet.IdleState);
        public void Walk() => Container.StateMachine.ChangeState(_personStateSet.WalkState);
        public void Run() => Container.StateMachine.ChangeState(_personStateSet.RunState);
        public void Jump() => Container.StateMachine.ChangeState(_personStateSet.JumpState);
        public void Roll() => Container.StateMachine.ChangeState(_personStateSet.RollState);
        public void Fall() => Container.StateMachine.ChangeState(_personStateSet.FallState);
        public void Slide() => Container.StateMachine.ChangeState(_personStateSet.SlideState);
        public void FallDown() => Container.StateMachine.ChangeState(_personStateSet.FallDownState);
        public void Die() => Container.StateMachine.ForcedChangeState(_personStateSet.DeathState);
    }
}