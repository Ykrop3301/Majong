using Cysharp.Threading.Tasks;

namespace Common.GameFSM
{
    public interface IGameStateMachine
    {
        public UniTask Enter<T>() where T: IGameState;
    }
}
