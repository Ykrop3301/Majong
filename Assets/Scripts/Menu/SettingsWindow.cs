using Common.Settings;
using Cysharp.Threading.Tasks;
using Menu;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameTemplate.Menu
{
    public class SettingsWindow : MenuElement
    {
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _sfxSlider;
        [SerializeField] private GameObject _view;

        private ISettingsService _settingsService;

        [Inject]
        public void Construct(ISettingsService settingsService)
            => _settingsService = settingsService;

        public void Show()
            => _view.SetActive(true);

        public void Hide() 
            => _view.SetActive(false);

        public void SetMusicVolume(float value)
            => _settingsService.SetMusicVolume(value);

        public void SetSFXVolume(float value)
            => _settingsService.SetSFXVolume(value);

        public void OnMusicSliderValueChanged()
            => SetMusicVolume(_musicSlider.value);

        public void OnSFXSliderValueChanged()
            => SetSFXVolume(_sfxSlider.value);

        public override UniTask Prepare()
        {
            _musicSlider.value = _settingsService.MusicVolume.Value;
            _sfxSlider.value = _settingsService.SFXVolume.Value;

            return UniTask.CompletedTask;
        }
    }
}