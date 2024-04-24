using Character.Classes;

namespace Character.StateMachine.CharacterStates.WarriorStates
{
    public class DoubleStrikeAttackState : ComboAttackState
    {
        private bool _canSecondStrike = true;
        private int _frames;

        public DoubleStrikeAttackState(Warrior warrior) : base(warrior)
        {
            Animation = "combo-attack";
        }

        public override void Execute()
        {
            base.Execute();
            SecondStrike();
        }

        private void SecondStrike()
        {
            if (_canSecondStrike) _frames++;
            if (_frames < Warrior.Container.Config.SecondStrikeDelay) return;

            _frames = 0;
            _canSecondStrike = false;
            
            ApplyDamageWithoutStamina();
        }

        private void ApplyDamageWithoutStamina()
        {
            var outputDamage = GetDamage();
            _weapon.DoDamage(outputDamage, GetEnemyMask());
        }
        
    }
}