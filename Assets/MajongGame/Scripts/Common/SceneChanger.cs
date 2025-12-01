using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MajongGame.Common
{
    public class SceneChanger : MonoBehaviour
    {
        [SerializeField] private Image _blackout;
        [SerializeField] private float _bloackoutDuration = 0.5f;

        private void Awake()
        {
            _blackout.gameObject.SetActive(false);
        }

        public void LoadScene(string sceneName)
        {
            StartCoroutine(LoadSceneCoroutine(sceneName));
        }

        private IEnumerator LoadSceneCoroutine(string sceneName)
        {
            GlobalVariablesController.CanClickOnTiles = false;
            _blackout.gameObject.SetActive(true);
            yield return _blackout.DOFade(1f, _bloackoutDuration).From(0f).WaitForCompletion();

            SceneManager.LoadScene(sceneName);

            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == sceneName);

            yield return _blackout.DOFade(0f, _bloackoutDuration).From(1f).WaitForCompletion();
            _blackout.gameObject.SetActive(false);
            GlobalVariablesController.CanClickOnTiles = true;
        }
    }
}
