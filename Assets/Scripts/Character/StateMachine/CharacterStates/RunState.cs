using Character.ComponentContainer;
using Global;

namespace Character.StateMachine.CharacterStates
{
    public class RunState : TiredState
    {
        public RunState(PersonContainer personContainer) : base(personContainer)
        {
            Animation = "run";
        }

        public override void Execute() => SideByVelocity();
        
        public override void FixedExecute()
        {
            base.FixedExecute();
            ChangingIndicators();
            PersonContainer.Movement.Run();
        }

        protected override void ChangingIndicators() => PersonContainer.Stamina.Decrease(PersonContainer.Config.StaminaUsage * GlobalConstants.RunStaminaUsageCoef);
    }
}