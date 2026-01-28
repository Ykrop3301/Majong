using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameTemplate.Menu
{
    public class PlayButton: MonoBehaviour
    {
        public void Play()
            => SceneManager.LoadScene("GameplayScene");
    }
}
