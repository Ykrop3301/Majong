using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameTemplate.Gameplay.UI
{
    public class MenuButton : MonoBehaviour
    {
        public void GoToMenu()
            => SceneManager.LoadScene("MenuScene");
    }
}