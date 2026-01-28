using UniRx;

namespace Common.Settings
{
    public interface ISettingsService
    {
        public IReadOnlyReactiveProperty<float> MusicVolume { get; }
        public IReadOnlyReactiveProperty<float> SFXVolume { get; }

        public void SetMusicVolume(float value);
        public void SetSFXVolume(float value);

        public void Dispose();
    }
}
