using Common.Save;
using System;
using UniRx;

namespace Common.Settings
{
    public class SettingsService : ISettingsService
    {
        public IReadOnlyReactiveProperty<float> MusicVolume { get; }
        public IReadOnlyReactiveProperty<float> SFXVolume { get; }

        private readonly ReactiveProperty<float> _musicVolume;
        private readonly ReactiveProperty<float> _sfxVolume;

        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        private readonly ISaveService _saveService;


        public SettingsService(ISaveService saveService)
        {
            _saveService = saveService;

            var savedMusic = Math.Clamp(_saveService.GetFloat("MusicVolume"), 0f, 1f);
            var savedSfx = Math.Clamp(_saveService.GetFloat("SFXVolume"), 0f, 1f);

            _musicVolume = new ReactiveProperty<float>(savedMusic);
            _sfxVolume = new ReactiveProperty<float>(savedSfx);

            MusicVolume = _musicVolume.ToReadOnlyReactiveProperty();
            SFXVolume = _sfxVolume.ToReadOnlyReactiveProperty();

            _musicVolume
                .Subscribe(value =>
                {
                    _saveService.SetFloat("MusicVolume", value);
                })
                .AddTo(_disposables);

            _sfxVolume
                .Subscribe(value =>
                {
                    _saveService.SetFloat("SFXVolume", value);
                })
                .AddTo(_disposables);
        }

        public void SetMusicVolume(float value) 
            => _musicVolume.Value = Math.Clamp(value, 0f, 1f);
        public void SetSFXVolume(float value) 
            => _sfxVolume.Value = Math.Clamp(value, 0f, 1f);

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
