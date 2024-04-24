using System.Collections;
using UnityEngine;

namespace Damageables.Traps
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class VerticalMotor : MonoBehaviour
    {
        [SerializeField] private bool _isActive;
        [SerializeField] private float _force;
        [SerializeField] private float _delayMin, _delayMax;

        private Rigidbody2D _rbody;
        private bool _isDelay;
        
        private void Start() => _rbody = GetComponent<Rigidbody2D>();

        private void Update()
        {
            if (_isActive) StartCoroutine(MotorEnable());
        }

        private IEnumerator MotorEnable()
        {
            if (_isDelay) yield break;
            
            _isDelay = true;
            _rbody.AddForce(transform.up * _force * _rbody.mass, ForceMode2D.Impulse);

            var delay = Random.Range(_delayMin, _delayMax);
            
            yield return new WaitForSeconds(delay);
            _isDelay = false;
        }
    }
}