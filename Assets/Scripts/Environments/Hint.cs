using Character.ComponentContainer;
using TMPro;
using UnityEngine;

namespace Environments
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Hint : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private BoxCollider2D _collider;

        private void Start() => _collider = GetComponent<BoxCollider2D>();
        
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.TryGetComponent(out PersonContainer person) && person.IsPlayer) SetAlpha(GetDistance(person.transform.position));
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out PersonContainer person) && person.IsPlayer) SetAlpha(0);
        }

        private float GetDistance(Vector2 position) => _collider.size.x / 2 - Vector2.Distance(position, transform.position);
        
        private void SetAlpha(float value)
        {
            var textColor = _text.color;
            textColor.a = value;
            _text.color = textColor;
        }
    }
}