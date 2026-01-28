using Common.AssetsProvider;
using Common.Curtain;
using Cysharp.Threading.Tasks;
using Menu;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Common.GameFSM
{
    public class MenuState : IGameState
    {
        private readonly ICurtain _curtain;
        private readonly IAssetsProvider _assetsProvider;

        public MenuState(ICurtain curtain, IAssetsProvider assetsProvider)
        {
            _curtain = curtain;
            _assetsProvider = assetsProvider;
        }

        public async UniTask Enter()
        {
            await PrepareMenu();
            await _curtain.Hide();
        }

        private async UniTask PrepareMenu()
        {
            List<GameObject> elements = await _assetsProvider.LoadAllAsync<GameObject>("MenuUI");

            Transform menuHolder = new GameObject("MenuHolder").GetComponent<Transform>();

            List<UniTask> tasks = new List<UniTask>();

            foreach (GameObject element in elements)
            {
                MenuElement newElement = ProjectContext.Instance.Container.InstantiatePrefabForComponent<MenuElement>(element, menuHolder).GetComponent<MenuElement>();
                tasks.Add(newElement.Prepare());
            }


            await UniTask.WhenAll(tasks);
        }

        public async UniTask OnExit()
        {
            await _curtain.Show();
        }
    }
}
