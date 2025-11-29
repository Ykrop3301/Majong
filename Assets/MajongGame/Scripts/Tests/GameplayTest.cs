using MajongGame.Common;
using MajongGame.Common.LevelSystem;
using MajongGame.Common.PopupSystem;
using MajongGame.Common.PopupSystem.PopupVariants;
using MajongGame.Configs.Level;
using MajongGame.LevelSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MajongGame.Tests
{
    public class GameplayTest : MonoBehaviour
    {
        [SerializeField] private List<LevelLocationConfig> _locations;
        [SerializeField] private CoroutineRunner _coroutineRunner;
        [SerializeField] private PopupsHolder _popupHolder;
        
        private ILevelsController _levelsController;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            _levelsController = new LevelsController(_locations, _coroutineRunner);
            _levelsController.PlayCurrentLevel();

            StartCoroutine(WaitGameplaySceneCoroutine());
        }

        private IEnumerator WaitGameplaySceneCoroutine()
        {
            yield return new WaitUntil(() =>SceneManager.GetActiveScene().name == "GameplayScene");

            InformationPopup popup = _popupHolder.GetPopup<InformationPopup>();
            popup.SetInfo("Test", "Test");
            popup.Show();
        }
    }
}