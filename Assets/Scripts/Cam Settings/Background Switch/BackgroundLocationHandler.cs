using Global;
using UnityEngine;

namespace Cam_Settings.Background_Switch
{
    [RequireComponent(typeof(Camera))]
    public class BackgroundLocationHandler : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
            EventBus.OnLocationSwitched.AddListener(SwitchBackground);
        }

        private void SwitchBackground(string name)
        {
            DisableChilds(_camera.transform);
            var backgrnd = _camera.transform.Find(name);
            if (backgrnd) backgrnd.gameObject.SetActive(true);
        }

        private void DisableChilds(Transform parent)
        {
            var childCount = parent.childCount;
            for (int i = 0; i < childCount; i++)
                parent.GetChild(i).gameObject.SetActive(false);
        }
    }
}