using Character.Classes;
using Global;

namespace Character.StateMachine.CharacterStates.WarriorStates
{
    public class ComboAttackState : AttackState
    {
        public ComboAttackState(Warrior warrior) : base(warrior)
        {
            Animation = "combo-attack";
        }

        protected override void ApplyDamage()
        {
            var outputDamage = GetDamage() * Warrior.Container.Config.ComboAttackDamageModificator;
           _weapon.DoDamage(outputDamage, GetEnemyMask());
            Warrior.Container.Stamina.Decrease(Warrior.Container.Config.StaminaAttackUsageCoef * outputDamage / GlobalConstants.StaminaComboDivider);
        }
    }
}