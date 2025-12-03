using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MajongGame.Gameplay
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [field: SerializeField] public Transform Transform;
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private ParticleSystem _particleSystem;

        public event System.Action<Tile> Died;
        public event System.Action<Tile> Selected;
        public Sprite Sprite { get; private set; }
        public bool IsTaked { get; private set; } = false;
        public TileAnimator Animator { get; private set; }
        public bool IsActive { get; private set; } = false;

        private TileColorChanger _colorChanger;
        private const float CHANGE_COLOR_DURATION = 0.2f;
        private List<Tile> _coveringTiles;

        public void SetTaked()
        {
            IsTaked = true;
            Selected?.Invoke(this);
        }

        private void SetActive(bool flag)
        {
            if (_colorChanger == null)
                _colorChanger = new TileColorChanger(_meshRenderer, CHANGE_COLOR_DURATION);

            IsActive = flag;
            Color newColor = flag ? Color.white : Color.gray / 2;

            StartCoroutine(_colorChanger.ChangeColorTo(newColor));
        }

        public void CheckActive()
        {
            BoxCollider boxCollider = GetComponent<BoxCollider>();
            Vector3 center = transform.TransformPoint(boxCollider.center);
            Vector3 boxCenter = center + Vector3.up * (boxCollider.size.y / 2);

            List<Tile> hits = Physics.OverlapBox(boxCenter, boxCollider.size / 2.1f)
                .Where(x => x.TryGetComponent(out Tile tile) && tile != this)
                .Select(x => x.GetComponent<Tile>())
                .ToList();

            if (hits.Count == 0)
            {
                SetActive(true);
            }
            else
            {
                _coveringTiles = new List<Tile>();

                foreach (Tile tile in hits)
                {
                    _coveringTiles.Add(tile);
                    tile.Selected += OnCoveringTileDied;
                }
            }
        }

        private void OnCoveringTileDied(Tile tile)
        {
            tile.Selected -= OnCoveringTileDied;
            _coveringTiles.Remove(tile);

            if (_coveringTiles.Count == 0)
            {
                SetActive(true);
            }
        }

        public void SetSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
            Sprite = sprite;
        }

        public void Die()
        {
            StartCoroutine(DieCoroutine());
        }

        private IEnumerator DieCoroutine()
        {
            _particleSystem.transform.parent = null;
            yield return Transform.DOScale(0f, 0.2f).From(1f);
            _particleSystem.Play();

            yield return new WaitUntil(() => _particleSystem.isStopped);

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