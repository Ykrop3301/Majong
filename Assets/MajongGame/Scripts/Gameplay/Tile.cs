using UnityEngine;

namespace MajongGame.Gameplay
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [field: SerializeField] public Transform Transform;

        public Sprite Sprite { get; private set; }
        public bool IsTaked { get; private set; } = false;

        public void SetTaked()
        {
            IsTaked = true;
        }

        public void SetSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
            Sprite = sprite;
        }
    }
}