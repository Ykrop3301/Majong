using UnityEngine;
using Zenject;

namespace Common.Save
{
    public class SaveOnQuitController : MonoBehaviour
    {
        private ISaveService _saveService;

        [Inject]
        public void Construct(ISaveService saveService)
        {
            _saveService = saveService;
        }

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void OnApplicationFocus(bool focus)
        {
            if (!focus)
                _saveService.Save();
        }

        private void OnApplicationQuit()
        {
            _saveService.Save();
        }
    }
}