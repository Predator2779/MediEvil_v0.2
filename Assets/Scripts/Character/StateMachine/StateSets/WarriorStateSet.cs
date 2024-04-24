using Character.Classes;
using Character.StateMachine.CharacterStates.WarriorStates;

namespace Character.StateMachine.StateSets
{
    public class WarriorStateSet : PersonStateSet
    {
        public AttackState AttackState { get; }
        public ComboAttackState ComboAttackState { get; }
        public CombatSlideState CombatSlideState { get; }
        public DefenseState DefenseState { get; }

        public WarriorStateSet(Warrior warrior) : base(warrior)
        {
            AttackState = new AttackState(warrior);
            ComboAttackState = new DoubleStrikeAttackState(warrior);
            CombatSlideState = new CombatSlideState(warrior);
            DefenseState = new DefenseState(warrior);
        }
    }
}