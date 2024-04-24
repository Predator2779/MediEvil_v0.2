using Character.Classes;
using Damageables.Weapons;

namespace Character.StateMachine.CharacterStates.ThrowerStates
{
    public class PullWeaponState : CharacterState
    {
        private Thrower Thrower { get; }
        private Weapon _weapon;

        public PullWeaponState(Thrower thrower) : base(thrower.Container)
        {
            Thrower = thrower;
        }

        public override bool CanEnter()
        {
            _weapon = Thrower.Container.WeaponHandler.DropedWeapon;
            return _weapon != null && Thrower.Container.WeaponHandler.CurrentWeapon == null;
        }
        
        public override void Enter()
        {
            base.Enter();
            _weapon.Pull(Thrower);
        }
    }
}