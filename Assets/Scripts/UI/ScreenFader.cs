using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ScreenFader : Fader
    {
        [SerializeField] private Image fader;
        
        public override void SetVolume(float volume)
        {
            if (IsDone(volume, fader.color.a))
            {
                SendDoneMessage();
                return;
            }

            StopAllCoroutines();
            StartCoroutine(DelayedMute(fader.color.a, volume));
        }
        
        protected override void SetAlpha(float alpha)
        {
            Color c = fader.color;
            c.a = alpha;
            fader.color = c;
        }
    }
}