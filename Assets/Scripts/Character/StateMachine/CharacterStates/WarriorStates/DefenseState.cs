using Character.Classes;

namespace Character.StateMachine.CharacterStates.WarriorStates
{
    public class DefenseState : WarriorState
    {
        public DefenseState(Warrior warrior) : base(warrior)
        {
            Animation = "defense";
        }

        public override void Enter()
        {
            base.Enter();
            Warrior.Container.Health.CanDamage = false;
        }

        public override void Execute()
        {
            base.Execute();
            SafetyCompleting();
        }

        public override void Exit()
        {
            base.Exit();
            Warrior.Container.Health.CanDamage = true;
        }
    }
}