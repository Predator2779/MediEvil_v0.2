using TMPro;
using UnityEngine;

namespace UI
{
    public class TextFader : Fader
    {
        [SerializeField] private TMP_Text fader;
        
        public override void SetVolume(float volume)
        {
            _alpha = fader.color.a;
            
            if (IsDone(volume, _alpha))
            {
                SendDoneMessage();
                return;
            }

            StopAllCoroutines();
            StartCoroutine(DelayedMute(volume, 1));
            // if (volume > fader.color.a) StartCoroutine(DelayedMute(volume, 1));
            // else if (volume < fader.color.a) StartCoroutine(DelayedMute(volume,1));
        }
        
        protected override void SetAlpha(float alpha)
        {
            Color c = fader.color;
            c.a = alpha;
            fader.color = c;
        }
    }
}