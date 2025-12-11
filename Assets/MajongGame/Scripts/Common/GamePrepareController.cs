using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace MajongGame.Common
{
    public class GamePrepareController : MonoBehaviour
    {
        private void Start()
        {
            Prepare();
            SceneManager.LoadScene("MenuScene");
        }

        private void Prepare()
        {
            if (!PlayerPrefs.HasKey("TilePrefabName"))
                PlayerPrefs.SetString("TilePrefabName", "DefaultTile");

            GlobalVariablesController.OnLoadingScene = false;
            GlobalVariablesController.LevelPreparing = false;
            GlobalVariablesController.InPopup = false;
            GlobalVariablesController.CanClickTiles = true;
        }
    }
}