using System;
using System.Collections;
using UnityEngine;

namespace UI
{
    public abstract class Fader : MonoBehaviour
    {
        [SerializeField] protected float duration = 1;

        public delegate void FaderEvent();
        public event FaderEvent OnFaderIsDone;
        
        public void Mute() => SetVolume(1);
        public void Unmute() => SetVolume(0);
        public abstract void SetVolume(float volume);

        protected IEnumerator DelayedMute(float startAlpha, float targetAlpha)
        {
            float startTime = Time.time;

            while (Time.time < startTime + duration)
            {
                float t = (Time.time - startTime) / duration;
                float newTransparency = Mathf.Lerp(startAlpha, targetAlpha, t);
                SetAlpha(newTransparency);
                yield return null;
            }
            
            SetAlpha(targetAlpha);
            SendDoneMessage();
        }

        protected bool IsDone(float volume,float alpha) => Math.Abs(volume - alpha) <= 0.01f;
        protected abstract void SetAlpha(float alpha);
        protected void SendDoneMessage() => OnFaderIsDone?.Invoke();
    }
}