using System.Threading.Tasks;
using Character.ComponentContainer;
using Global;

namespace Character.StateMachine.CharacterStates
{
    public class FallDownState : CharacterState
    {
        public FallDownState(PersonContainer personContainer) : base(personContainer)
        {
            Animation = "death";
        }

        public override void Enter()
        {
            IsCompleted = false;
            base.Enter();
        }

        public override void Execute()
        {
            if (!AnimationCompleted()) Task.Delay(GlobalConstants.FallDownDelay).ContinueWith(_ => IsCompleted = true);
        }
    }
}