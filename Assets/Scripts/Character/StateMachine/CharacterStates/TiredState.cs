using Character.ComponentContainer;

namespace Character.StateMachine.CharacterStates
{
    public class TiredState : CharacterState
    {
        public override bool CanEnter() => PersonContainer.Stamina.CanUse;

        public TiredState(PersonContainer personContainer) : base(personContainer)
        {
        }
    }
}