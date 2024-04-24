using System.Collections;
using Character.ComponentContainer;
using UnityEngine;

namespace Environments
{
    [RequireComponent(typeof(SliderJoint2D))]
    public class MotorSwitcher : MonoBehaviour, IInteractable
    {
        [SerializeField] private float _delay;
        
        private SliderJoint2D _slider;
        private void Start() => _slider = GetComponent<SliderJoint2D>();

        public void Interact()
        {
            var motor = _slider.motor;
            motor.motorSpeed *= -1;
            _slider.motor = motor;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.TryGetComponent(out PersonContainer _)) StartCoroutine(Delay());
        }

        private IEnumerator Delay()
        {
            yield return new WaitForSeconds(_delay);
            Interact();
        }
    }
}