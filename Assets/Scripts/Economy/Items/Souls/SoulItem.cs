using UnityEngine;

namespace Economy.Items.Souls
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
            _isInvoked = true;
        }

        public override void Put()
        {
            throw new System.NotImplementedException();
        }

        private void SetInitParameters()
        {
            _animator = GetComponent<Animator>();
            var scale = Mathf.Clamp(Count / 1000, 0.5f, 3.0f);
            transform.localScale = new Vector3(scale, scale, scale);
        }

        private void DisableItem() => gameObject.SetActive(false);
    }
}