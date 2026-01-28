using PlayerPrefs = RedefineYG.PlayerPrefs;

namespace Common.Save
{
    public class YGPlayerPrefsSaveService : ISaveService
    {
        public void DeleteAll()
            => PlayerPrefs.DeleteAll();

        public void DeleteKey(string key)
            => PlayerPrefs.DeleteKey(key);

        public float GetFloat(string key)
            => PlayerPrefs.GetFloat(key);

        public int GetInt(string key)
            => PlayerPrefs.GetInt(key);

        public string GetString(string key)
            => PlayerPrefs.GetString(key);

        public bool HasKey(string key)
            => PlayerPrefs.HasKey(key);

        public void Save()
            => PlayerPrefs.Save();

        public void SetFloat(string key, float value)
            => PlayerPrefs.SetFloat(key, value);

        public void SetInt(string key, int value)
            => PlayerPrefs.SetInt(key, value);

        public void SetString(string key, string value)
            => PlayerPrefs.SetString(key, value);

        
    }
}
