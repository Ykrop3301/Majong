using System;
using UniRx;
using UnityEngine;

namespace Gameplay.Tiles
{
    public class Tile : MonoBehaviour, ITakeable, IHaveSprite
    {
        [field: SerializeField] public Transform Transform { get; private set; }
        [SerializeField] private SpriteRenderer _spriteRenderer;
        public IReadOnlyReactiveProperty<bool> Taked => _taked;

        private ReactiveProperty<bool> _taked = new ReactiveProperty<bool>();


        public void OnTaked()
        {
            _taked.Value = true;
        }

        public void ResetValues()
        {
            
        }

        public void Show()
        {

        }

        public void OnCantTake()
        {

        }

        public void SetSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }
    }
}