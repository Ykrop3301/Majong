using MajongGame.Common.PopupSystem;
using MajongGame.Configs.Level;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MajongGame.Common.LevelSystem
{
    public class LevelsController : ILevelsController
    {
        private readonly List<LevelLocationConfig> _locations;
        private readonly LevelRunner _levelRunner;

        public (LevelLocationConfig location, int levelId) CurrentLevel { get; private set; }

        public LevelsController(List<LevelLocationConfig> locations, CoroutineRunner coroutineRunner, PopupsHolder popupsHolder, SceneChanger sceneChanger)
        {
            _locations = locations;

            if (!PlayerPrefs.HasKey("UnlockedLocations"))
            {
                PlayerPrefs.SetString("UnlockedLocations", _locations.First().Name);
                PlayerPrefs.SetInt("UnlockedLevelsCount" + _locations.First().Name, 1);
                PlayerPrefs.SetString("CurrentLocation", _locations.First().Name);
            }

            _levelRunner = new LevelRunner(coroutineRunner, this, popupsHolder, sceneChanger);
        }

        public LevelLocationConfig GetLocation(string locationName)
        {
            if (!_locations.Select(x => x.Name).Contains(locationName)) 
                throw new System.Exception("Invalid location name.");

            return _locations.Where(x => x.Name == locationName).First();
        }

        public void PlayLevel(string locationName, int levelId)
        {
            if (!_locations.Select(x => x.Name).Contains(locationName))
                throw new System.Exception("Invalid location name.");

            string[] unlockedLocations = PlayerPrefs.GetString("UnlockedLocations").Split(',');

            if (!unlockedLocations.Contains(locationName))
                throw new System.Exception($"Location {locationName} is not unlocked");

            if (PlayerPrefs.GetInt("UnlockedLevelsCount" + locationName) <= levelId)
                throw new System.Exception($"Level {levelId} is not unlocked");

            CurrentLevel = (GetLocation(locationName), levelId);

            _levelRunner.RunLevel(CurrentLevel.location, CurrentLevel.levelId);
        }

        public void PlayNextLevel()
        {
            if (CurrentLevel.levelId + 1 >= CurrentLevel.location.LevelsCount)
                throw new System.Exception("Invalid level id.");

            PlayLevel(CurrentLevel.location.Name, CurrentLevel.levelId + 1);
        }

        public void UnlockNextLevel()
        {
            if (CurrentLevel.levelId + 1 < CurrentLevel.location.LevelsCount)
            {
                int unlockedLevelsCount = PlayerPrefs.GetInt("UnlockedLevelsCount" + CurrentLevel.location.Name);

                PlayerPrefs.SetInt("UnlockedLevelsCount" + CurrentLevel.location.Name, unlockedLevelsCount + 1);

                PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + CurrentLevel.location.Reward);
            }
            else
            {
                string unlockedLocations = PlayerPrefs.GetString("UnlockedLocations");
                int nextLocationId = _locations.IndexOf(CurrentLevel.location) + 1;
                if (_locations.Count <= nextLocationId)
                    return;

                unlockedLocations += $",{_locations[nextLocationId].Name}";
                PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + CurrentLevel.location.Reward);
                PlayerPrefs.SetString("UnlockedLocations", unlockedLocations);
                PlayerPrefs.SetString("CurrentLocation", _locations[nextLocationId].Name);
                PlayerPrefs.SetInt("UnlockedLevelsCount" + _locations[nextLocationId].Name, 1);
            }
        }

        public void PlayCurrentLevel()
        {
            string currentLocation = PlayerPrefs.GetString("CurrentLocation");

            CurrentLevel = (GetLocation(currentLocation), PlayerPrefs.GetInt("UnlockedLevelsCount" + currentLocation) - 1);
            PlayLevel(CurrentLevel.location.Name, CurrentLevel.levelId);
        }
    }
}
