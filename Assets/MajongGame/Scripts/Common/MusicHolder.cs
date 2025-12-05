using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MajongGame.Common
{
    public class MusicHolder : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private List<AudioClip> _clips;

        private string _lastSceneName;
        private AudioClip _currentAudioClip;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            StartCoroutine(WaitSceneChangedCoroutine());
        }

        private IEnumerator WaitSceneChangedCoroutine()
        {
            while (true)
            {
                _lastSceneName = SceneManager.GetActiveScene().name;
                yield return new WaitUntil(() => SceneManager.GetActiveScene().name != _lastSceneName);
                SetNewClip();
            }
        }

        private void SetNewClip()
        {
            AudioClip newClip = _clips[Random.Range(0, _clips.Count)];
            if (newClip != _currentAudioClip)
            {
                _audioSource.clip = newClip;
                _currentAudioClip = newClip;
                _audioSource.Play();
            }
        }
    }
}