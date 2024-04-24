using UnityEngine;

namespace Other
{
    public class SpriteChanger : MonoBehaviour
    {
        [SerializeField] private Sprite _newSprite;

        private SpriteRenderer _spriteRenderer;

        private void Start() => _spriteRenderer = GetComponent<SpriteRenderer>();
        public void ChangeSprite() => _spriteRenderer.sprite = _newSprite;
    }
}