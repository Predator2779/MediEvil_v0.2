using System.Collections;
using UnityEngine;

namespace VFX
{
    [RequireComponent(typeof(Animator))]
    public class VFXPlayer : MonoBehaviour
    {
        [SerializeField] private float _completingDelay;
        
        public delegate void CompletingAnimation(VFXPlayer vfxPlayer);
        public CompletingAnimation OnAnimationComplete;
        
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Play(string clipName)
        {
            _animator.CrossFade(clipName, 0);
            StartCoroutine(CompleteAnimation());
        }

        private IEnumerator CompleteAnimation()
        {
            yield return new WaitForSeconds(_completingDelay);
            OnAnimationComplete?.Invoke(this);
        }
    }
}