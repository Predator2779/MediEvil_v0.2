using ModestTree;
using TMPro;
using UnityEngine;

namespace Web
{
    [RequireComponent(typeof(TMP_Text))]
    public class TextLinkTransition : LinkTransition
    {
        private TMP_Text _tmp;

        private void Awake() => _tmp = GetComponent<TMP_Text>();
        
        public void GoToLink()
        {
            if (_tmp != null && !_tmp.text.IsEmpty()) Application.OpenURL(_tmp.text);
        }
    }
}