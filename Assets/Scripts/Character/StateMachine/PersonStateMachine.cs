using Character.StateMachine.CharacterStates;

namespace Character.StateMachine
{
    public class PersonStateMachine
    {
        public PersonStateMachine(CharacterState defaultState)
        {
            CurrentState = defaultState;
        }

        public CharacterState CurrentState { get; set; }

        public void ChangeState(CharacterState newState)
        {
            if (CurrentState == newState || !newState.CanEnter() ||
                !CurrentState.IsCompleted || newState.IsCooldown) return;

            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }

        public void ForcedChangeState(CharacterState newState)
        {
            if (CurrentState == newState) return;

            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }

        public void Execute() => CurrentState.Execute();
        public void FixedExecute() => CurrentState.FixedExecute();
    }
}