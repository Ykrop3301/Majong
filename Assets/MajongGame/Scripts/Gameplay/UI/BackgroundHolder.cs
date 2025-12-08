using UnityEngine;
using UnityEngine.UI;

namespace MajongGame.Gameplay.UI
{
    public class BackgroundHolder : MonoBehaviour
    {
        [SerializeField] private Image _bg;

        public void SetBG(Sprite bg)
        {
            _bg.sprite = bg;
        }
    }
}