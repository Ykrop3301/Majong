using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace MajongGame.Common
{
    public class GamePrepareController : MonoBehaviour
    {
        private SceneChanger _sceneChanger;

        [Inject]
        private void Construct(SceneChanger sceneChanger)
        {
            _sceneChanger = sceneChanger;
        }

        private void Start()
        {
            Prepare();
            _sceneChanger.LoadScene("MenuScene");
        }

        private void Prepare()
        {
            if (!PlayerPrefs.HasKey("TilePrefabName"))
                PlayerPrefs.SetString("TilePrefabName", "DefaultTile");
        }
    }
}