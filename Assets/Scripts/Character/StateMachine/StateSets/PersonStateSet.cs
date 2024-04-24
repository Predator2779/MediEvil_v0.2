using Character.Classes;
using Character.StateMachine.CharacterStates;

namespace Character.StateMachine.StateSets
{
    public class PersonStateSet
    {
        public CharacterState DefaultState { get; set; }
        public IdleState IdleState { get; }
        public WalkState WalkState { get; }
        public RunState RunState { get; }
        public JumpState JumpState { get; }
        public FallState FallState { get; }
        public FallDownState FallDownState { get; }
        public RollState RollState { get; }
        public SlideState SlideState { get; }
        public DeathState DeathState { get; }

        public PersonStateSet(Person person)
        {
            IdleState = new IdleState(person.Container);
            WalkState = new WalkState(person.Container);
            RunState = new RunState(person.Container);
            JumpState = new JumpState(person.Container);
            RollState = new RollState(person.Container);
            FallState = new FallState(person.Container);
            FallDownState = new FallDownState(person.Container);
            SlideState = new SlideState(person.Container);
            DeathState = new DeathState(person);

            DefaultState = IdleState;
        }
    }
}