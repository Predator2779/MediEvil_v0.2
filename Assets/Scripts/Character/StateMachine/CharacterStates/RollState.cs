using Character.ComponentContainer;
using Global;

namespace Character.StateMachine.CharacterStates
{
    public class RollState : TiredState
    {
        public RollState(PersonContainer personContainer) : base(personContainer)
        {
            Animation = "roll";
        }

        public override void Enter()
        {
            base.Enter();
            PersonContainer.Movement.Roll();
            PersonContainer.Health.CanDamage = false;
            PersonContainer.DustEffectPlayer.PlayStep();
            PersonContainer.Stamina.Decrease(PersonContainer.Config.StaminaUsage *
                                             GlobalConstants.RollStaminaUsageCoef);
        }

        public override void Execute()
        {
            SafetyCompleting();
            SideByVelocity();
        }

        public override void Exit()
        {
            base.Exit();
            PersonContainer.Movement.StopVelocity();
            PersonContainer.Health.CanDamage = true;
        }

        public override void FixedExecute()
        {
        }
    }
}