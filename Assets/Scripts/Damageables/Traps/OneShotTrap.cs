using UnityEngine;

namespace Damageables.Traps
{
    public class OneShotTrap : Trap
    {
        [SerializeField] private bool _disableCollider;

        private Collider2D _collider;

        private void Start() => _collider = GetComponent<Collider2D>();

        protected override void EnableTrap(Collider2D collider)
        {
            base.EnableTrap(collider);
            if (_disableCollider && _collider != null) Destroy(_collider);
            Destroy(this);
        }
    }
}