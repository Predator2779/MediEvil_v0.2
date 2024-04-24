using Character.Classes;
using Character.StateMachine.CharacterStates.ThrowerStates;

namespace Character.StateMachine.StateSets
{
    public class ThrowerStateSet : PersonStateSet
    {
        public ThrowState ThrowState { get; }
        public TwistedThrowState TwistedThrowState { get; }
        public PullWeaponState PullWeaponState { get; set; }

        public ThrowerStateSet(Thrower thrower) : base(thrower)
        {
            ThrowState = new ThrowState(thrower);
            TwistedThrowState = new TwistedThrowState(thrower);
            PullWeaponState = new PullWeaponState(thrower);
        }
    }
}