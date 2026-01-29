using Common.GameFSM;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace GameTemplate.Menu
{
    public class PlayButton: MonoBehaviour
    {
        private IGameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public async void Play()
        {
            await _gameStateMachine.Enter<GameplayState>();

            SceneManager.LoadScene("GameplayScene");
        }
    }
}
