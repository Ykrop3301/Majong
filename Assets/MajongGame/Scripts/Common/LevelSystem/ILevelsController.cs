using MajongGame.Configs.Level;

namespace MajongGame.Common.LevelSystem
{
    public interface ILevelsController
    {
        public void UnlockNextLevel();
        public void PlayLevel(string locationName, int levelNum);
        public void PlayNextLevel();
        public void PlayCurrentLevel();
        public LevelLocationConfig GetLocation(string locationName);
        public (LevelLocationConfig location, int levelId) CurrentLevel { get; }
    }
}
