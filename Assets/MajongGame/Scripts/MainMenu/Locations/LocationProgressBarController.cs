using MajongGame.Configs.Level;
using UnityEngine;
using UnityEngine.UI;

namespace MajongGame.MainMenu.Locations
{
    public class LocationProgressBarController : MonoBehaviour
    {
        [SerializeField] private Image _bar;

        public void SetLocation(LevelLocationConfig location)
        {
            int levelsCount = location.LevelsCount;
            int finishedLevelsCount = PlayerPrefs.GetInt("UnlockedLevelsCount" + location.Name) - 1;
            float progressPercentage = (float)finishedLevelsCount / (float)levelsCount;
            _bar.fillAmount = progressPercentage;

            Color newColor = GetCurrentColor(progressPercentage);
            _bar.color = newColor;
        }

        private Color GetCurrentColor(float progressPercentage)
        {
            if (progressPercentage > 0.8f)
                return Color.green;
            else if (progressPercentage > 0.4f)
                return Color.yellow;
            else return Color.red;
        }
    }
}