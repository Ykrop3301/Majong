using GameTemplate.Configs;
using UnityEngine;
using UnityEngine.UI;

namespace GameTemplate.Menu
{
    public class LocationHolder : MonoBehaviour
    {
        [field: SerializeField] public Transform Transform { get; private set; }
        [SerializeField] private Image _image;

        public LocationConfig LocationConfig { get; private set; }

        public void SetLocation(LocationConfig location)
        {
            LocationConfig = location;
            _image.sprite = LocationConfig.Icon;
        }
    }
}