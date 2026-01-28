using Cysharp.Threading.Tasks;

namespace Common.GameFSM
{
    public class GameplayState : IGameState
    {
        private readonly IGameStateMachine _gameStateMachine;

        public GameplayState(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        public UniTask Enter()
        {
            throw new System.NotImplementedException();
        }

        public UniTask OnExit()
        {
            throw new System.NotImplementedException();
        }
    }
}
