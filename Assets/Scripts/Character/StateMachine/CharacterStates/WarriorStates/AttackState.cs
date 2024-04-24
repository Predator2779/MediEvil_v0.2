using Character.Classes;
using Damageables.Weapons;
using Global;
using UnityEngine;

namespace Character.StateMachine.CharacterStates.WarriorStates
{
    public class AttackState : WarriorState
    {
        protected Weapon _weapon;

        public AttackState(Warrior warrior) : base(warrior)
        {
            Animation = "attack";
        }

        public override void Enter()
        {
            base.Enter();
            SetWeapon();
            ApplyDamage();
        }

        public override void Execute()
        {
            base.Execute();
            SafetyCompleting();
            CooldownControl();
            EndAttackCallback();
        }

        private void SetWeapon() => _weapon = Warrior.Container.WeaponHandler.CurrentWeapon;

        private void EndAttackCallback()
        {
            if (GetPercentCurrentMomentAnim() >= 100) Warrior.OnEndedAttack?.Invoke();
        }

        protected float GetDamage()
        {
            var baseDamage = Warrior.Container.Config.Damage;
            return Mathf.Clamp(baseDamage * GetVelocityModificator(), baseDamage,
                baseDamage * GetVelocityModificator());
        }

        protected virtual void ApplyDamage()
        {
            var outputDamage = GetDamage();
            _weapon.DoDamage(outputDamage, GetEnemyMask());
            Warrior.Container.Stamina.Decrease(Warrior.Container.Config.StaminaAttackUsageCoef * outputDamage);
        }

        private float GetVelocityModificator() => Mathf.Abs(Warrior.Container.Movement.GetVelocity().x +
                                                            Warrior.Container.Movement.GetVelocity().y) *
                                                  GlobalConstants.VelocityDamageCoef;

        public override bool CanEnter() =>
            Warrior.Container.WeaponHandler.CurrentWeapon != null && Warrior.Container.Stamina.CanUse;

        protected LayerMask GetEnemyMask() => LayerMask.GetMask(Warrior.Container.IsPlayer ? "Enemy" : "Player");
    }
}