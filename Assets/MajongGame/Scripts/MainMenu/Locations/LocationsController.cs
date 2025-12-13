using DG.Tweening;
using MajongGame.Configs.Level;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

namespace MajongGame.MainMenu.Locations
{
    public class LocationsController : MonoBehaviour
    {
        [SerializeField] private List<LevelLocationConfig> _locations;
        [SerializeField] private PlayButtonController _playButtonController;
        [SerializeField] private ParticleSystem _particleSystem;

        [SerializeField] private Transform _leftPoint;
        [SerializeField] private Transform _rightPoint;
        [SerializeField] private Transform _centerPoint;

        [SerializeField] private LocationHolder _currentLocation;
        [SerializeField] private LocationHolder _futureLocation;
        [SerializeField] private LocationProgressBarController _progressBarController;
        [SerializeField] private float _moveDuration = 0.4f;

        private void Start()
        {
            LevelLocationConfig currentLocation = _locations
                .Where(x => x.Name == PlayerPrefs.GetString("UnlockedLocations").Split(',').Last())
                .First();

            PlayerPrefs.SetString("CurrentLocation", currentLocation.Name);
            _currentLocation.SetLocation(currentLocation);
            _currentLocation.Transform.position = _centerPoint.position;

            _particleSystem.Stop();
            _particleSystem.textureSheetAnimation.SetSprite(0, currentLocation.ParticleSprite);
            _particleSystem.Play();

            _progressBarController.gameObject.SetActive(true);
            _progressBarController.SetLocation(currentLocation);
        }

        public void ShowNextLocation()
        {
            int currentLocationId = _locations.IndexOf(_currentLocation.LocationConfig);
            LevelLocationConfig nextLocation = currentLocationId + 1 >= _locations.Count ? _locations.First() : _locations[currentLocationId + 1];

            _futureLocation.SetLocation(nextLocation);
            _futureLocation.Transform.position = _rightPoint.position;

            _currentLocation.Transform.DOMove(_leftPoint.position, _moveDuration).From(_centerPoint.position);
            _futureLocation.Transform.DOMove(_centerPoint.position, _moveDuration).From(_rightPoint.position);

            (_currentLocation, _futureLocation) = (_futureLocation, _currentLocation);
            SetCurrentLocation(_currentLocation.LocationConfig);
        }

        public void ShowPreviousLocation()
        {
            int currentLocationId = _locations.IndexOf(_currentLocation.LocationConfig);
            LevelLocationConfig nextLocation = currentLocationId - 1 < 0 ? _locations.Last() : _locations[currentLocationId - 1];

            _futureLocation.SetLocation(nextLocation);
            _futureLocation.Transform.position = _leftPoint.position;

            _currentLocation.Transform.DOMove(_rightPoint.position, _moveDuration).From(_centerPoint.position);
            _futureLocation.Transform.DOMove(_centerPoint.position, _moveDuration).From(_leftPoint.position);

            (_currentLocation, _futureLocation) = (_futureLocation, _currentLocation);
            SetCurrentLocation(_currentLocation.LocationConfig);
        }

        private void SetCurrentLocation(LevelLocationConfig location)
        {
            string[] unlocedLocations = PlayerPrefs.GetString("UnlockedLocations").Split(',');
            

            _particleSystem.Clear();
            _particleSystem.textureSheetAnimation.SetSprite(0, location.ParticleSprite);
            _particleSystem.Play();

            foreach (string locationString in unlocedLocations)
                if (locationString == location.Name)
                {
                    PlayerPrefs.SetString("CurrentLocation", location.Name);
                    _playButtonController.SetActive(true);

                    _progressBarController.gameObject.SetActive(true);
                    _progressBarController.SetLocation(location);
                    return;
                }

            _playButtonController.SetActive(false);
            _playButtonController.SetBuyingLocaion(location);
            _progressBarController.gameObject.SetActive(false);
        }
    }
}