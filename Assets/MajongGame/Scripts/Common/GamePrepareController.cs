using UnityEngine;
using UnityEngine.SceneManagement;

namespace MajongGame.Common
{
    public class GamePrepareController : MonoBehaviour
    {
        private void Awake()
        {
            Prepare();
            SceneManager.LoadScene("MenuScene");
        }

        private void Prepare()
        {
            if (!PlayerPrefs.HasKey("TilePrefabName"))
                PlayerPrefs.SetString("TilePrefabName", "DefaultTile");
        }
    }
}