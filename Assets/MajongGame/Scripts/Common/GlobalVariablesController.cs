namespace MajongGame.Common
{
    public static class GlobalVariablesController
    {
        public static bool OnLoadingScene { get; set; } = false;
        public static bool OnLevelPreparing { get; set; } = false;
        public static bool InPopup { get; set; } = false;
    }
}
