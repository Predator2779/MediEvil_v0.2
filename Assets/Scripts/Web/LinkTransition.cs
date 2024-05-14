using UnityEngine;

namespace Web
{
    public class LinkTransition : MonoBehaviour
    {
        public void GoToLink(string url) => Application.OpenURL(url);
    }
}