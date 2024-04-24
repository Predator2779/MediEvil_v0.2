using System.Threading.Tasks;
using Character.ComponentContainer;
using Global;

namespace Character.StateMachine.CharacterStates
{
    public abstract class CharacterState
    {
        protected PersonContainer PersonContainer { get; }
        protected string Animation { get; set; }

        public bool IsCooldown;
        public bool IsCompleted = true;

        protected CharacterState(PersonContainer personContainer)
        {
            PersonContainer = personContainer;
        }

        public virtual bool CanEnter() => !PersonContainer.IsDeath;

        public virtual void Enter() =>
            PersonContainer.Animator.CrossFade(Animation, GlobalConstants.SpeedCrossfadeAnim);

        public virtual void Execute()
        {
        }

        public virtual void FixedExecute() => ChangingIndicators();
        protected void SideByVelocity() => PersonContainer.Movement.SetSideByVelocity();
        protected void SafetyCompleting() => IsCompleted = AnimationCompleted();
        public virtual void Exit() => PersonContainer.Animator.StopPlayback();

        protected void CooldownControl()
        {
            if (PersonContainer.Stamina.CanUse || IsCooldown) return;
            IsCooldown = true;
            Task.Delay(PersonContainer.Config.StaminaRestoreDelay).ContinueWith(_ => IsCooldown = false);
        }

        protected bool AnimationCompleted()
        {
            var animInfo = PersonContainer.Animator.GetCurrentAnimatorStateInfo(0);
            return animInfo.normalizedTime >= animInfo.length + GlobalConstants.AnimDelay;
        }

        /// <summary>
        /// The method returns the completed animation path as a percentage.
        /// </summary>
        /// <returns>current animation moment</returns>
        protected float GetPercentCurrentMomentAnim()
        {
            var animInfo = PersonContainer.Animator.GetCurrentAnimatorStateInfo(0);
            return (animInfo.normalizedTime / animInfo.length + GlobalConstants.AnimDelay) * 100;
        }

        protected virtual void ChangingIndicators()
        {
            if (PersonContainer.Stamina.CanRestore())
                PersonContainer.Stamina.Increase(PersonContainer.Config.StaminaRecovery);
        }
    }
}