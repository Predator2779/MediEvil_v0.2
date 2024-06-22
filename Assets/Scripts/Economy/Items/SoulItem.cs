using System;
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
        
        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

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

        private void DestroyItem() => Destroy(gameObject);
    }
}