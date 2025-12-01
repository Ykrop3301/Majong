using MajongGame.Common;
using MajongGame.Common.LevelSystem;
using MajongGame.Common.PopupSystem;
using MajongGame.Common.PopupSystem.PopupVariants;
using MajongGame.Configs.Level;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace MajongGame.Tests
{
    public class GameplayTest : MonoBehaviour
    {
        [Inject] private ILevelsController _levelsController;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            _levelsController.PlayCurrentLevel();
            //StartCoroutine(WaitGameplaySceneCoroutine());
        }

        private IEnumerator WaitGameplaySceneCoroutine()
        {
            yield return new WaitUntil(() =>SceneManager.GetActiveScene().name == "GameplayScene");
        }
    }
}