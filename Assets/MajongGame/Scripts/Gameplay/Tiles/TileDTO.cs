using UnityEngine;

namespace MajongGame.Gameplay.Tiles
{
    public class TileDTO
    {
        public Transform Transform { get; set; }
        public Sprite Sprite { get; set; }
        public bool IsTaked { get; private set; } = false;
        public bool IsActive { get; set; } = false;
        public bool IsDead { get; set; } = false;

        public event System.Action Taked;
        public event System.Action<bool> ActiveChanged;
        public event System.Action Dead;

        public TileDTO(Transform transform)
        {
            Transform = transform;
        }

        public void SetTaked()
        {
            IsTaked = true;
            Taked?.Invoke();
        }

        public void SetActive(bool active)
        {
            IsActive = active;
            ActiveChanged?.Invoke(IsActive);
        }

        public void SetDied()
        {
            IsDead = true;
            Dead?.Invoke();
        }
    }
}
