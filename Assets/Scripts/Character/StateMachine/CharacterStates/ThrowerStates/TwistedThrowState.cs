using Character.Classes;
using Global;
using UnityEngine;

namespace Character.StateMachine.CharacterStates.ThrowerStates
{
    public class TwistedThrowState : ThrowState
    {
        public TwistedThrowState(Thrower thrower) : base(thrower)
        {
        }

        protected override void Throw()
        {
            base.Throw();
            _weapon.GetRBody().AddTorque(GlobalConstants.ThrowTorque, ForceMode2D.Force);
        }
    }
}