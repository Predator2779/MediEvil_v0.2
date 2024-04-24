using Character.ComponentContainer;

namespace Character.StateMachine.CharacterStates
{
    public class IdleState : CharacterState
    {
        public IdleState(PersonContainer personContainer) : base(personContainer)
        {
            Animation = "idle";
        }
    }
}