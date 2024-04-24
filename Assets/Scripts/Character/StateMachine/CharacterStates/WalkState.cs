using Character.ComponentContainer;

namespace Character.StateMachine.CharacterStates
{
    public class WalkState : CharacterState
    {
        public WalkState(PersonContainer personContainer) : base(personContainer)
        {
            Animation = "walk";
        }

        public override void Execute() => SideByVelocity();

        public override void FixedExecute()
        {
            base.FixedExecute();
            PersonContainer.Movement.Walk();
        }
    }
}