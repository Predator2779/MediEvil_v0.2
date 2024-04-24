using UnityEngine;

namespace Other
{
    public class ObjectActiveSwitcher : MonoBehaviour
    {
        [SerializeField] private bool _activeFlag;
        [SerializeField] private GameObject[] _objects;

        private void OnTriggerEnter2D(Collider2D other)
        {
            foreach (var o in _objects) o.SetActive(_activeFlag);
        }
    }
}