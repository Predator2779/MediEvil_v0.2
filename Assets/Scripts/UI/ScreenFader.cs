using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ScreenFader : Fader
    {
        [SerializeField] private Image fader;
        
        public override void SetVolume(float volume)
        {
            _alpha = fader.color.a;
            
            if (IsDone(volume, _alpha))
            {
                SendDoneMessage();
                print("done");
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