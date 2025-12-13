using MajongGame.Common.LevelSystem;
using TMPro;
using UnityEngine;
using Zenject;

namespace ButchersGames
{
    namespace CheatMenu
    {
        public class LvlInputField : MonoBehaviour
        {
            // Замени на свою функцию
            [Inject] private static ILevelsController _levelController;
            [Inject] private ILevelsController _levelControllerNonStatic;

            static void LoadLvl(int lvl)
            {
                _levelController.PlayLevel(PlayerPrefs.GetString("CurrentLocation"), lvl);
            }

            void Awake()
            {
                GetComponent<TMP_InputField>().onEndEdit.AddListener(LoadLvl);
                _levelController = _levelControllerNonStatic;
            }
            void LoadLvl(string lvl) { if (int.TryParse(lvl, out int lvlID)) LoadLvl(lvlID); }
        }
    }
}