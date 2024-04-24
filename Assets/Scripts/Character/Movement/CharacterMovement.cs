using Character.ComponentContainer;
using Character.Configs;
using Global;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Character.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class CharacterMovement : MonoBehaviour
    {
        public CapsuleCollider2D Capsule { get; private set; }
        public Vector2 Direction { get; set; }
        public Vector2 TempDirection { get; set; } = new Vector2(1, 0);
        public Vector2 ContactNormal { get; set; }

        [SerializeField] private float _requireAngle = 50;
        [SerializeField] private float _drawRadius = 0.02f;
        [SerializeField] private float _drawLine = 1;

        private Rigidbody2D _rbody;
        private CharacterConfig _config;
        private Vector2 _contactPoint;
        private ContactPoint2D _contact;
        private ContactPoint2D[] _contacts;

        private CompositeDisposable _disposables;

        private void Awake()
        {
            SetComponents();
            this.OnCollisionStay2DAsObservable().Subscribe(SetContacts);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (Capsule == null) return;

            var pos = new Vector2(Capsule.transform.position.x, RequireOffset());

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(pos, _drawRadius);

            if (_contacts != null)
            {
                foreach (var contact in _contacts)
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawSphere(contact.point, _drawRadius);
                }
            }

            Gizmos.color = Color.green;
            Gizmos.DrawSphere(_contact.point, _drawRadius);

            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(_contactPoint, _drawRadius);

            Gizmos.color = Color.blue;
            Gizmos.DrawRay(_contactPoint, ContactNormal * _drawLine);
        }
#endif

        private void SetComponents()
        {
            Capsule = GetComponent<CapsuleCollider2D>();
            _rbody = GetComponent<Rigidbody2D>();
            _config = GetComponent<PersonContainer>().Config;
        }

        private void SetContacts(Collision2D collision)
        {
            _contacts = collision.contacts;
            _contact = GetNearestPoint(_contacts);

            if (Vector2.Distance(_contact.point, _contactPoint) <= GlobalConstants.PointOffset ||
                !CorrectAngle(_contact.normal)) return;

            _contactPoint = _contact.point;
            ContactNormal = _contact.normal;
        }

        private ContactPoint2D GetNearestPoint(ContactPoint2D[] contacts)
        {
            var length = contacts.Length;
            var position = Capsule.transform.position;
            var contact = contacts[0];
            var value = Vector2.Distance(position, contacts[0].point);

            for (int i = 0; i < length; i++)
            {
                var newValue = Vector2.Distance(position, contacts[i].point);

                if (newValue >= value) continue;

                contact = contacts[i];
                value = newValue;
            }

            return contact;
        }

        private float RequireOffset() =>
            Capsule.transform.position.y +
            Capsule.offset.y - Capsule.size.y / 2 +
            GlobalConstants.CollisionOffset;

        private bool CorrectAngle(Vector2 normal) => Vector2.Angle(normal, Vector2.up) <= _requireAngle;
        
        public void Walk() =>
            _rbody.velocity = GetHorizontalDirection(_config.SpeedMove * GlobalConstants.CoefPersonSpeed);

        public void Run() =>
            _rbody.velocity = GetHorizontalDirection(_config.SpeedRun * GlobalConstants.CoefPersonSpeed);

        public void FallMove() => _rbody.velocity =
            GetHorizontalDirection(_config.SpeedMove * _config.FallSpeed * GlobalConstants.HorizontalFallMoveSpeed);

        public void Jump() => _rbody.AddForce(GetJumpVector() * _config.JumpForce * _rbody.mass, ForceMode2D.Impulse);
        public void Roll() => _rbody.velocity = GetRollVector();
        public void Slide() => _rbody.AddForce(GetSlideVector() * _config.SlideSpeed, ForceMode2D.Impulse);
        public void SetBodyType(RigidbodyType2D type) => _rbody.bodyType = type;

        public bool IsGrounded() => _contactPoint.y <= _rbody.position.y + GlobalConstants.CollisionOffset &&
                                    _rbody.position.y - _contactPoint.y <= GlobalConstants.MaxGroundOffset;

        public bool IsFall() => _rbody.velocity.y < -GlobalConstants.FallSpeed;
        public bool CanSlide() => Mathf.Abs(GetVelocity().x) >= _config.SlideLimitVelocity;
        public void SetSideByVelocity() => RotateObj(GetVelocity().x < 0 ? -1 : 0);
        private void RotateObj(float angle) => transform.localRotation = new Quaternion(0, angle, 0, 0);

        public void LookTo(Transform target) =>
            transform.localRotation = new Quaternion(0, GetTargetSide(target), 0, 0);

        private float GetTargetSide(Transform target) => target.transform.position.x < transform.position.x ? -1 : 0;
        private Vector2 GetHorizontalDirection(float speed) => new Vector2(Direction.x * speed, _rbody.velocity.y);

        private Vector2 GetRollVector() =>
            new Vector2(TempDirection.normalized.x * _config.RollDistance, _config.RollHeight);

        private Vector2 GetSlideVector() => new Vector2(_rbody.velocity.x, _rbody.velocity.y);
        private Vector2 GetJumpVector() => new Vector2(Direction.normalized.x * GlobalConstants.HorizontalJumpCoef, 1);
        public Vector2 GetVelocity() => _rbody.velocity;
    }
}