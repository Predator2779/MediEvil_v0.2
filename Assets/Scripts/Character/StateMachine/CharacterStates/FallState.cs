using Character.ComponentContainer;

namespace Character.StateMachine.CharacterStates
{
    public class FallState : CharacterState
    {
        public FallState(PersonContainer personContainer) : base(personContainer)
        {
            Animation = "fall";
        }

        public override void FixedExecute()
        {
            base.FixedExecute();
            PersonContainer.Movement.FallMove();
        }
    }
}