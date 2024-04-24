using Character.Classes;
using Damageables.Weapons;
using UnityEngine;

namespace Character.StateMachine.CharacterStates.ThrowerStates
{
    public class ThrowState : TiredState
    {
        private Thrower Thrower { get; }
        protected Weapon _weapon;

        public ThrowState(Thrower thrower) : base(thrower.Container)
        {
            Thrower = thrower;
        }

        public override bool CanEnter() =>
            Thrower.Container.WeaponHandler.CurrentWeapon != null && Thrower.Container.Stamina.CanUse;
        
        public override void Enter()
        {
            base.Enter();
            Throw();
        }

        protected virtual void Throw()
        {
            _weapon = Thrower.Container.WeaponHandler.CurrentWeapon;

            if (_weapon == null) return;

            Thrower.Container.WeaponHandler.DropWeapon();
            _weapon.Throw(Thrower, GetThrowVector().normalized * GetThrowForce());
        }

        #region another class

        private Vector2 GetThrowVector() => GetMousePos() - (Vector2) Thrower.Container.transform.position;
        private float GetThrowForce() => Thrower.Container.Config.ThrowForce * _weapon.GetRBody().mass;

        private Vector2 GetMousePos() /// require refactoring
        {
            var mousePos = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition); /// divide responsibility
            var position = new Vector2();
            var transform = Thrower.Container.transform;

            position.x = Mathf.Clamp(mousePos.x, -Camera.main.pixelWidth / 2, Camera.main.pixelWidth / 2);
            position.y = Mathf.Clamp(mousePos.y, -Camera.main.pixelHeight / 2, Camera.main.pixelHeight / 2);

            var sign = Mathf.Sign(transform.rotation.y);
            var min = transform.position.x;
            var max = transform.position.x + 100;

            if (sign >= 0)
                return new Vector2(Mathf.Clamp(position.x, min, max),
                    position.y);

            return new Vector2(Mathf.Clamp(position.x, -max, min),
                position.y);
        }

        #endregion
    }
}