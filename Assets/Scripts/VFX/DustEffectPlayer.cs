using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace VFX
{
    public class DustEffectPlayer : MonoBehaviour
    {
        [SerializeField] private GameObject _vfxPrefab;
        [SerializeField] private AnimationClip[] _stepClips;
        [SerializeField] private AnimationClip[] _jumpClips;

        private Queue<VFXPlayer> _stepsPull;
        private Queue<VFXPlayer> _jumpsPull;

        private void Awake()
        {
            _stepsPull = CreatePull(_stepClips);
            _jumpsPull = CreatePull(_jumpClips);
        }

        public void PlayStep() => EnableVFX(_stepsPull, _stepClips);
        public void PlayJump() => EnableVFX(_jumpsPull, _jumpClips);

        private void EnableVFX(Queue<VFXPlayer> pull, AnimationClip[] clips)
        {
            if (pull.Count <= 0) return;

            var vfx = pull.Dequeue();
            vfx.OnAnimationComplete += pull.Enqueue;
            vfx.transform.position = transform.position;
            vfx.transform.rotation = transform.rotation;
            vfx.Play(GetRandomAnim(clips));
        }

        private Queue<VFXPlayer> CreatePull(AnimationClip[] clips)
        {
            var length = clips.Length;
            Queue<VFXPlayer> pull = new Queue<VFXPlayer>();
            var parent = FindOrCreatePath("VFXDusts_Pull");
            
            for (int i = 0; i < length; i++)
            {
                var clone = Instantiate(_vfxPrefab, transform.position, Quaternion.identity, parent);
                pull.Enqueue(clone.GetComponent<VFXPlayer>());
            }

            return pull;
        }

        private Transform FindOrCreatePath(string name)
        {
            var path = GameObject.Find(name);
            
            if (path == null)
            {
                path = new GameObject();
                path.transform.position = Vector3.zero;
                path.name = name;
            }
            
            return path.transform;
        }
        
        private string GetRandomAnim(AnimationClip[] clips) => clips[Random.Range(0, clips.Length)].name;
    }
}