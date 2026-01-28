using Common.AssetsProvider;
using Common.Save;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameTemplate.Configs;
using Menu;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;


namespace GameTemplate.Menu
{
    public class LocationSelector : MenuElement
    {
        [SerializeField] private Transform _leftPoint;
        [SerializeField] private Transform _rightPoint;
        [SerializeField] private Transform _centerPoint;

        [SerializeField] private float _moveDuration = 0.4f;

        [SerializeField] private LocationHolder _currentLocation;
        [SerializeField] private LocationHolder _futureLocation;

        private ISaveService _saveService;
        private IAssetsProvider _assetsProvider;
        private List<LocationConfig> _locations;


        [Inject]
        public void Construct(ISaveService saveService, IAssetsProvider assetsProvider)
        {
            _saveService = saveService;
            _assetsProvider = assetsProvider;
        }

        public void ShowNextLocation()
        {
            int currentLocationId = _locations.IndexOf(_currentLocation.LocationConfig);
            LocationConfig nextLocation = currentLocationId + 1 >= _locations.Count ? _locations.First() : _locations[currentLocationId + 1];

            SetLocation(nextLocation, _rightPoint.position, _leftPoint.position);
        }

        public void ShowPreviousLocation()
        {
            int currentLocationId = _locations.IndexOf(_currentLocation.LocationConfig);
            LocationConfig nextLocation = currentLocationId - 1 < 0 ? _locations.Last() : _locations[currentLocationId - 1];

            SetLocation(nextLocation, _leftPoint.position, _rightPoint.position);
        }

        private void SetLocation(LocationConfig selectedLocation, Vector3 futurePosition, Vector3 newCurrentPosition)
        {
            AnimateSettingLocation(selectedLocation, futurePosition, newCurrentPosition);

            (_currentLocation, _futureLocation) = (_futureLocation, _currentLocation);

            _saveService.SetString("CurrentLocation", selectedLocation.Id);
        }

        private void AnimateSettingLocation(LocationConfig selectedLocation, Vector3 futurePosition, Vector3 newCurrentPosition)
        {
            _futureLocation.SetLocation(selectedLocation);
            _futureLocation.Transform.position = futurePosition;

            _currentLocation.Transform.DOMove(newCurrentPosition, _moveDuration).From(_centerPoint.position);
            _futureLocation.Transform.DOMove(_centerPoint.position, _moveDuration).From(futurePosition);
        }

        public override async UniTask Prepare()
        {
            _locations = await _assetsProvider.LoadAllAsync<LocationConfig>("Locations");

            if (!_saveService.HasKey("CurrentLocation"))
                _saveService.SetString("CurrentLocation", _locations[0].Id);

            LocationConfig currentLocationConfig = _locations.Where(x => x.Id == _saveService.GetString("CurrentLocation")).First();
            _currentLocation.SetLocation(currentLocationConfig);
            await UniTask.CompletedTask;
        }
    }
}