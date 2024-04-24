using UnityEngine;

namespace Other.Follow
{
    public class Parallax : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] [Range(0, 1)] private float _parallaxStrength = 0.1f;
        [SerializeField] private bool _moveWithConstantSpeed;
        [SerializeField] private bool _disableVerticalParallax = true;
        [SerializeField] private bool _invertDirection;

        private Vector3 _targetPrevPosition;

        private void Start()
        {
            if (!_target) _target = Camera.main.transform;
            _targetPrevPosition = _target.position;
        }

        private void LateUpdate()
        {
            if (_moveWithConstantSpeed) MoveWithConstantSpeed();
            else MoveWithParallax();
        }

        private void MoveWithParallax()
        {
            var direction = -1;
            var delta = _target.position - _targetPrevPosition;

            if (_disableVerticalParallax) delta.y = 0;
            if (_invertDirection) direction *= -1;

            _targetPrevPosition = _target.position;
            transform.position += delta * _parallaxStrength * direction;
        }

        private void MoveWithConstantSpeed()
        {
            var direction = -1;

            if (_invertDirection) direction *= -1;
            
            var vector = new Vector3(direction, _disableVerticalParallax ? 0 : direction, 0);
            transform.position += vector * _parallaxStrength * Time.deltaTime;
        }
    }
}