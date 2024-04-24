using System.Collections.Generic;
using System.Threading.Tasks;
using Character.Classes;
using Global;
using UnityEngine;

namespace Character.StateMachine.CharacterStates
{
    public class DeathState : CharacterState
    {
        private Person _person;
        private bool _isRespawned;

        public DeathState(Person person) : base(person.Container)
        {
            Animation = "death";
            _person = person;
        }

        public override void Enter()
        {
            base.Enter();
            IsCompleted = false;
        }

        public override void Execute()
        {
            if (AnimationCompleted()) Die();
            if (_isRespawned) Respawn();
        }

        public override void FixedExecute()
        {
        }

        private void Die()
        {
            if (_person.Container.IsDeath) return;
            _person.Container.IsDeath = true;

            _person.Describe();
            PersonContainer.Movement.SetBodyType(RigidbodyType2D.Static);

            if (PersonContainer.IsPlayer)
                Task.Delay(PersonContainer.Config.TimeToRespawn)
                    .ContinueWith(_ => _isRespawned = true);
        }

        private void Respawn() //// to another class
        {
            PersonContainer.Movement.SetBodyType(RigidbodyType2D.Dynamic);
            PersonContainer.IsDeath = false;
            _isRespawned = false;

            PersonContainer.transform.position = GetNearestPoint(PersonContainer.Config.SavePoints);
            PersonContainer.Health.TakeFullHeal();
            
            IsCompleted = true;
        }

        private Vector2 GetNearestPoint(List<Vector2> points) // refactoring
        {
            if (points.Count <= 0) return GlobalConstants.StartPointPosition;

            var length = points.Count;
            var position = PersonContainer.transform.position;
            var point = GlobalConstants.StartPointPosition;
            var distance = Vector2.Distance(position, point);

            for (int i = 0; i < length; i++)
            {
                var newPoint = points[i];
                var newDistance = Vector2.Distance(position, newPoint);

                if (newDistance > distance) continue;

                point = newPoint;
                distance = newDistance;
            }

            return point;
        }
    }
}