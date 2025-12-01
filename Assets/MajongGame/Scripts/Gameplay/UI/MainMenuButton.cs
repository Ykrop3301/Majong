using MajongGame.Common;
using MajongGame.Common.PopupSystem;
using MajongGame.Common.PopupSystem.PopupVariants;
using UnityEngine;
using Zenject;

namespace MajongGame.Gameplay.UI
{
    public class MainMenuButton : MonoBehaviour
    {
        private SceneChanger _sceneChanger;
        private PopupsHolder _popupsHolder;

        [Inject]
        public void Construct(SceneChanger sceneChanger, PopupsHolder popupsHolder)
        {
            _sceneChanger = sceneChanger;
            _popupsHolder = popupsHolder;
        }

        public void GoToMainMenu()
        {
            ConfirmActionPopup confirmPopup = _popupsHolder.GetPopup<ConfirmActionPopup>();
            confirmPopup.SetConfirm(() => _sceneChanger.LoadScene("MenuScene"), "выйти в главное меню");
            confirmPopup.Show();
        }
    }
}