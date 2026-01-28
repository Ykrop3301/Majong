using Cysharp.Threading.Tasks;

namespace Common.GameFSM
{
    public interface IGameState
    {
        public UniTask Enter();
        public UniTask OnExit();
    }
}
