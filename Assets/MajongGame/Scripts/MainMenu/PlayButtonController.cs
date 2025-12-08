using MajongGame.Configs.Level;
using UnityEngine;

namespace MajongGame.MainMenu
{
    public class PlayButtonController : MonoBehaviour
    {
        [SerializeField] private CurrentLevelButton _levelButton;
        [SerializeField] private BuyLocationButton _buyLocationButton;

        private void Start()
        {
            _levelButton.gameObject.SetActive(true);
            _buyLocationButton.gameObject.SetActive(false);
        }

        public void SetActive(bool active)
        {
            _levelButton.gameObject.SetActive(active);
            _buyLocationButton.gameObject.SetActive(!active);
        }

        public void SetBuyingLocaion(LevelLocationConfig buyingLocaion)
        {
            _buyLocationButton.SetLocation(buyingLocaion);
        }
    }
}