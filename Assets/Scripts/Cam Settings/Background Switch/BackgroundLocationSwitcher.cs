using Character.ComponentContainer;
using Global;
using UnityEngine;

namespace Cam_Settings.Background_Switch
{
    public class BackgroundLocationSwitcher : MonoBehaviour
    {
        [SerializeField] private GameObject _backgroundPrefab;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PersonContainer person)) 
                EventBus.OnLocationSwitched?.Invoke(_backgroundPrefab.name);
        }
    }
}