using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Common.AssetsProvider
{
    public class AddressablesAssetsProvider : IAssetsProvider
    {
        public async UniTask<List<T>> LoadAllAsync<T>(string label) where T : Object
        {
            var results = new List<T>();

            return (await Addressables.LoadAssetsAsync<T>(
                label,
                result => results.Add(result),
                Addressables.MergeMode.Union
            )).ToList();
        }

        public async UniTask<T> LoadAsync<T>(string id) where T : Object
        {
            return await Addressables.LoadAssetAsync<T>(id);
        }
    }
}
