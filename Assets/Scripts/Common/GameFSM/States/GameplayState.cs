using Common.Curtain;
using Cysharp.Threading.Tasks;

namespace Common.GameFSM
{
    public class GameplayState : IGameState
    {
        private readonly ICurtain _curtain;

        public GameplayState(ICurtain curtain)
        {
            _curtain = curtain;
        }
        public async UniTask Enter()
        {
            await _curtain.Hide();
        }

        public async UniTask OnExit()
        {
            await _curtain.Show();
        }
    }
}
