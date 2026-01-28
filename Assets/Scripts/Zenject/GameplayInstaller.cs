using Common.Save;
using UnityEngine;
using Zenject;

namespace GameTemplate.Zenject
{
    public class GameplayInstaller : MonoInstaller
    {
        private ISaveService _saveService;

        [Inject]
        private void Construct(ISaveService saveService)
        {
            _saveService = saveService;
        }

        public override void InstallBindings()
        {
            Debug.Log($"Gameplay installed with location {_saveService.GetString("CurrentLocation")}");
        }
    }
}