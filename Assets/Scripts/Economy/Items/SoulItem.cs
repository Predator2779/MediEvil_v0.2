using Character.ComponentContainer;
using Global;
using UnityEngine;

namespace Economy.Items
{
    public class SoulItem : Item
    {
        [SerializeField] private int _volume;

        private Animator _animator;
        private bool _isInvoked;

        private void Awake() => SetInitParameters();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PersonContainer container)) PickUp(container);
        }

        private void PickUp(PersonContainer container)
        {
            if (_isInvoked) return;

            _animator.SetBool("IsPicked", true);
            container.SoulWallet.Increase(_volume);
            EventBus.OnSoulPicked?.Invoke();
            _isInvoked = true;
        }

        private void SetInitParameters()
        {
            _animator = GetComponent<Animator>();
            var scale = Mathf.Clamp(_volume / 1000, 0.5f, 3.0f);
            transform.parent.localScale = new Vector3(scale, scale, scale);
        }

        private void DestroyItem() => Destroy(gameObject);
    }
}