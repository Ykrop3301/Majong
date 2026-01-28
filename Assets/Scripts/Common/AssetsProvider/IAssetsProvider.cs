using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace Common.AssetsProvider
{
    public interface IAssetsProvider
    {
        public UniTask<T> LoadAsync<T>(string id) where T : Object;
        public UniTask<List<T>> LoadAllAsync<T>(string label) where T : Object;
    }
}
