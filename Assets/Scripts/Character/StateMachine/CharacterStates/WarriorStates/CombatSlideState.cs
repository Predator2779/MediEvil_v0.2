using Character.Classes;
using Character.ComponentContainer;
using Character.ValueStorages;
using Global;
using UnityEngine;

namespace Character.StateMachine.CharacterStates.WarriorStates
{
    public class CombatSlideState : SlideState
    {
        private readonly Warrior _warrior;

        private bool _hasEnemies;

        public CombatSlideState(Warrior warrior) : base(warrior.Container)
        {
            _warrior = warrior;
        }

        public override void Execute()
        {
            base.Execute();
            CheckEnemies();
        }

        public override void Exit()
        {
            base.Exit();
            _hasEnemies = false;
        }

        private void CheckEnemies()
        {
            if (_hasEnemies) return;

            var pos = (Vector2) PersonContainer.transform.position + new Vector2(0.25f, 0.1f); //// magic nums
            var colliders = Physics2D.OverlapCircleAll(pos, 0.25f); //// magic num

            if (colliders == null) return;
            
            _hasEnemies = true;
            ApplyDamage(colliders);
        }

        private void ApplyDamage(Collider2D[] colliders)
        {
            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out PersonContainer person) && !person.IsPlayer)
                    DoDamage(person.Health, GetDamage());
            }
        }

        private float GetDamage()
        {
            var baseDamage = _warrior.Container.Config.Damage * _warrior.Container.Config.CombatSlideDamage;
            return Mathf.Clamp(baseDamage * GetVelocityModificator(), baseDamage,
                baseDamage * GetVelocityModificator());
        }

        private void DoDamage(Health health, float concreteDamage) => health.TakeDamage(concreteDamage);

        private float GetVelocityModificator() => Mathf.Abs(_warrior.Container.Movement.GetVelocity().x +
                                                            _warrior.Container.Movement.GetVelocity().y) *
                                                  GlobalConstants.VelocityDamageCoef;
    }
}