using Character.ComponentContainer;
using UnityEngine;

namespace Other
{
    public class DeathZone : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PersonContainer person)) person.Health.Die();
        }
    }
}