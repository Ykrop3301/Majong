using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Menu
{
    public abstract class MenuElement : MonoBehaviour
    {
        public abstract UniTask Prepare();
    }
}
