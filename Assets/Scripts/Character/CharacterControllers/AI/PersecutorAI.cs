using Character.ComponentContainer;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Character.CharacterControllers.AI
{
    public class PersecutorAI : Controller
    {
        protected float _viewingRadius = 3;
        protected float _runDistance = 2.5f;
        protected float _walkDistance = 1.5f;
        protected float _stayDistance = 0.5f;
        protected LayerMask _layerMask = 3;
        protected Collider2D _target;
        
        private CapsuleCollider2D _capsule;

        public PersecutorAI(PersonContainer container, ScopeCoverage scopeCoverage) : base(container)
        {
            if (scopeCoverage == null) return;
            
            _viewingRadius = scopeCoverage.ViewingRadius;
            _runDistance = scopeCoverage.RunDistance;
            _walkDistance = scopeCoverage.WalkDistance;
            _stayDistance = scopeCoverage.StayDistance;
            _layerMask = scopeCoverage.LayerMask;
        }

        public override void Initialize()
        {
            base.Initialize();
            _capsule = _person.Container.Movement.Capsule;
        }

        public override void Execute()
        {
            base.Execute();

            SetTempDirection();
            
            if (!HasTarget() || CanStay())
            {
                Idle();
                return;
            }

            _person?.Container.Movement.LookTo(_target.transform);
            
            if (CanWalkFollow())
            {
                WalkFollow();
                return;
            }  
            
            if (CanRunFollow()) RunFollow();
        }

        protected bool HasTarget()
        {
            _target = Physics2D.OverlapCircle(GetCapsuleCenterPos(), _viewingRadius, _layerMask);
            return _target && !_target.GetComponent<PersonContainer>().IsDeath;
        }
        
        protected bool CanStay() => GetTargetDistance() <= _stayDistance;
        protected bool CanWalkFollow() => GetTargetDistance() <= _walkDistance;
        protected bool CanRunFollow() => GetTargetDistance() <= _runDistance;

        private void Roll() => _person.Roll();
        private void Jump() => _person.Jump();

        protected void RunFollow()
        {
            if (Random.Range(0, _person.Container.Config.RollChanceAI) == 0)
            {
                if (_person.Container.Config.CanRoll) Roll();
                else Jump();
            }
            else _person.Run();
        }
        
        protected void WalkFollow() => _person.Walk();
        protected void Idle() => _person.Idle();

        protected void SetTempDirection()
        {
            if (_target == null) return;

            _person.Container.Movement.Direction = GetTargetVector();
            _person.Container.Movement.TempDirection = GetTargetVector();
        }

        protected float GetTargetDistance() => Vector2.Distance(_person.Container.transform.position, _target.transform.position);
        private Vector2 GetTargetVector() => _target.transform.position - _person.Container.transform.position;

        private Vector2 GetCapsuleCenterPos() =>
            new Vector2(_capsule.transform.position.x,
                _capsule.transform.position.y + _capsule.size.y / 2);
    }
}