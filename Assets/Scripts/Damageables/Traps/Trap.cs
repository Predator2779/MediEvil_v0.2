using Character.ComponentContainer;
using Character.ValueStorages;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Damageables.Traps
{
    public class Trap : MonoBehaviour
    {
        [SerializeField] private float _damageMin, _damageMax;
        private PersonContainer _personContainer;
        
        private void OnTriggerEnter2D(Collider2D other) => EnableTrap(other);

        public void DoDamage(float personDamage) => _personContainer.Health.TakeDamage(personDamage);
        public void DoDamage(Health health, float concreteDamage) => health.TakeDamage(concreteDamage);
        private float GetDamageValue() => Random.Range(_damageMin, _damageMax);

        protected virtual void EnableTrap(Collider2D collider)
        {
            if (collider.gameObject.TryGetComponent(out _personContainer)) DoDamage(_personContainer.Health, GetDamageValue());
        }
    }
}