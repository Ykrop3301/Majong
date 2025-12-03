using UnityEngine;

namespace MajongGame.Gameplay
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [field: SerializeField] public Transform Transform;

        public event System.Action<Tile> Died;

        public Sprite Sprite { get; private set; }
        public bool IsTaked { get; private set; } = false;

        public TileAnimator Animator { get; private set; }

        public void SetTaked()
        {
            IsTaked = true;
        }

        public void SetSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
            Sprite = sprite;
        }

        public void Die()
        {
            Died?.Invoke(this);
            Destroy(gameObject);
        }

        private void OnEnable()
        {
            if (Animator == null)
                Animator = new TileAnimator(Transform);

            StartCoroutine(Animator.ShowTile());
        }
    }
}