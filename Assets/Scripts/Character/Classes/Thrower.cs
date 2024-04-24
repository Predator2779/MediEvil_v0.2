using Character.ComponentContainer;
using Character.StateMachine.StateSets;

namespace Character.Classes
{
    public class Thrower : Person
    {
        public Thrower(PersonContainer container) : base(container)
        {
        }
        
        private ThrowerStateSet _throwerStateSet;

        public override void Initialize()
        {
            base.Initialize();
            _throwerStateSet = new ThrowerStateSet(this);
        }

        public void ThrowWeapon() => Container.StateMachine.ChangeState(_throwerStateSet.ThrowState);
        public void ThrowWeaponTwisted() => Container.StateMachine.ChangeState(_throwerStateSet.TwistedThrowState);
        public void PullWeapon() => Container.StateMachine.ChangeState(_throwerStateSet.PullWeaponState);
    }
}