using MajongGame.Common;
using UnityEngine;

namespace MajongGame.Scris.Tests
{
    public class Debugger : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                Debug.Log($"InPopup={GlobalVariablesController.InPopup}," +
                    $" OnLevelPreparing={GlobalVariablesController.LevelPreparing}," +
                    $"OnLoadingScene={GlobalVariablesController.OnLoadingScene}," +
                    $"CanClickTiles={GlobalVariablesController.CanClickTiles},");
            }
        }
    }
}