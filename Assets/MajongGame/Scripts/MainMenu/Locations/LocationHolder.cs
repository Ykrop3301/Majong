using MajongGame.Configs.Level;
using UnityEngine;
using UnityEngine.UI;

namespace MajongGame.MainMenu.Locations
{
    public class LocationHolder : MonoBehaviour
    {
        [field: SerializeField] public Transform Transform {  get; private set; }
        [SerializeField] private Image _image;

        public LevelLocationConfig LocationConfig { get; private set; }

        public void SetLocation(LevelLocationConfig location)
        {
            LocationConfig = location;
            _image.sprite = LocationConfig.LocationImage;
        }
    }
}