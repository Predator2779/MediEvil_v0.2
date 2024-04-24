using Character.Classes;

namespace Character.StateMachine.CharacterStates.WarriorStates
{
    public class WarriorState : TiredState
    {
        protected Warrior Warrior { get; }

        public WarriorState(Warrior warrior) : base(warrior.Container)
        {
            Warrior = warrior;
        }
    }
}