namespace Common.Save
{
    public interface ISaveService
    {
        public void Save();
        public bool HasKey(string key);
        public void DeleteKey(string key);
        public void DeleteAll();

        public void SetString(string key, string value);
        public string GetString(string key);
        
        public void SetInt(string key, int value);
        public int GetInt(string key);

        public float GetFloat(string key);
        public void SetFloat(string key, float value);
    }
}
