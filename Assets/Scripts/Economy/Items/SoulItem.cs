using Global;
using UnityEngine;

namespace Economy.Items
{
    public class SoulItem : Item
    {
        private Animator _animator;
        private bool _isInvoked;

        private void Awake() => SetInitParameters();

        public override void PickUp()
        {
            if (_isInvoked) return;

            _animator.SetBool("IsPicked", true);
            EventBus.OnSoulPicked?.Invoke(Count);
            _isInvoked = true;
        }

        private void SetInitParameters()
        {
            _animator = GetComponent<Animator>();
            var scale = Mathf.Clamp(Count / 1000, 0.5f, 3.0f);
            transform.parent.localScale = new Vector3(scale, scale, scale);
        }

        private void DestroyItem() => Destroy(gameObject);
    }
}