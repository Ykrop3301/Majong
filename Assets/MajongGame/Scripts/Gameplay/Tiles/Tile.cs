using System.Collections;
using UnityEngine;

namespace MajongGame.Gameplay.Tiles
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private AudioSource _audioSource;

        private TileColorChanger _colorChanger;
        private TileSpriteController _spriteController;
        private TileAudioController _audioController;
        private TileAnimator _animator;
        private TileCoverChecker _coverChecker;
        private TileDeathController _deathController;

        public TileDTO TileDTO { get; private set; }
        public bool IsPlayingAnimation => _animator.IsPlaying;

        private void Awake()
        {
            TileDTO = new TileDTO(GetComponent<Transform>());

            _spriteController = new TileSpriteController(_spriteRenderer, TileDTO);
            _audioController = new TileAudioController(TileDTO, _audioSource);
            _colorChanger = new TileColorChanger(_meshRenderer, TileDTO, this);
            _coverChecker = new TileCoverChecker(TileDTO, this);
            _animator = new TileAnimator(TileDTO, this);
            _deathController = new TileDeathController(TileDTO, _particleSystem, this, _animator);
        }

        public void SetSprite(Sprite sprite)
        {
            _spriteController.SetSprite(sprite);
        }

        public void Show()
        {
            _animator.PlayOnTileSpawned();
            _audioController.Play("TileTaked", Random.Range(0.8f, 1f));
        }

        private IEnumerator WaitAnimationStopped(System.Action action)
        {
            yield return new WaitUntil(() => !_animator.IsPlaying);
            action();
        }

        public bool TryTake()
        {
            if (TileDTO.IsActive)
            {
                TileDTO.SetTaked();
                return true;
            }
            else
            {
                _animator.PlayOnCantTakeTile();
                return false;
            }
        }

        public void CheckCoveringTiles()
        {
            _coverChecker.CheckCoveringTiles();
        }

        public void Die()
        {
            TileDTO.SetDied();
        }

        private void OnEnable()
        {
            TileDTO.Transform.localScale = Vector3.zero;
        }
    }
}