using System;
using System.Collections;
using UnityEngine;

namespace UI
{
    public abstract class Fader : MonoBehaviour
    {
        [SerializeField] protected float fadeSpeed = 1;

        public delegate void FaderEvent();
        public event FaderEvent OnFaderIsDone;

        protected float _alpha;
        
        public void Mute() => SetVolume(1);
        public void Unmute() => SetVolume(0);
        public abstract void SetVolume(float volume);

        protected IEnumerator DelayedMute(float targetTransparency, float duration)
        {
            float currentTransparency = _alpha;
            float startTime = Time.time;

            while (Time.time < startTime + duration)
            {
                float t = (Time.time - startTime) / duration;
                float newTransparency = Mathf.Lerp(currentTransparency, targetTransparency, t);
                SetAlpha(newTransparency);
                yield return null;
            }
            
            SetAlpha(targetTransparency);
            SendDoneMessage();
        }

        protected bool IsDone(float volume,float alpha) => Math.Abs(volume - alpha) <= 0.01f;
        protected abstract void SetAlpha(float alpha);

        protected void SendDoneMessage()
        {
            OnFaderIsDone?.Invoke();
        }
    }
}