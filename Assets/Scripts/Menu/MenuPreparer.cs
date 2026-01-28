using Common.AssetsProvider;
using UnityEngine;
using Zenject;

namespace GameTemplate.Menu
{
    public class MenuPreparer : MonoBehaviour
    {
        private IAssetsProvider _assetsProvider;

        [Inject]
        private void Construct(IAssetsProvider assetsProvider)
        {
            _assetsProvider = assetsProvider;
        }
    }
}