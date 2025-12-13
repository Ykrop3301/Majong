using MajongGame.Common.PopupSystem;
using MajongGame.Common.PopupSystem.PopupVariants;
using MajongGame.Configs.Level;
using MajongGame.MainMenu.Locations;
using TMPro;
using UnityEngine;
using Zenject;

namespace MajongGame.MainMenu
{
    public class BuyLocationButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text _costTextField;
        [SerializeField] private PlayButtonController _playButtonController;
        [SerializeField] private LocationProgressBarController _barController;

        private PopupsHolder _popupsHolder;
        private LevelLocationConfig _locationConfig;

        [Inject]
        private void Construct(PopupsHolder popupsHolder)
        {
            _popupsHolder = popupsHolder;
        }

        public void SetLocation(LevelLocationConfig location)
        {
            _costTextField.text = location.Cost.ToString();
            _locationConfig = location;
        }

        public void Buy()
        {
            if (PlayerPrefs.GetInt("Money") >= _locationConfig.Cost)
            {
                ConfirmActionPopup confirmPopup = _popupsHolder.GetPopup<ConfirmActionPopup>();
                confirmPopup.SetConfirm(ConfirmBuying, $"купить локацию {_locationConfig.Name} за {_locationConfig.Cost} монет");
                confirmPopup.Show();
            }
            else
            {
                InformationPopup popup = _popupsHolder.GetPopup<InformationPopup>();
                popup.SetInfo("Внимание", "Не достаточно монет.");
                popup.Show();
            }
        }

        private void ConfirmBuying()
        {
            PlayerPrefs.SetString("UnlockedLocations", PlayerPrefs.GetString("UnlockedLocations") + $",{_locationConfig.Name}");
            PlayerPrefs.SetInt("UnlockedLevelsCount" + _locationConfig.Name, 1);

            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - _locationConfig.Cost);
            PlayerPrefs.SetString("CurrentLocation", _locationConfig.Name);
            _playButtonController.SetActive(true);
            _barController.gameObject.SetActive(true);
            _barController.SetLocation(_locationConfig);
        }
    }
}