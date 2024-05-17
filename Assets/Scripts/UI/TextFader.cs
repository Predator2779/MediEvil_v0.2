using TMPro;
using UnityEngine;

namespace UI
{
    public class TextFader : Fader
    {
        [SerializeField] private TMP_Text fader;
        
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