using Common.GameFSM;
using UnityEngine;
using Zenject;

namespace Bootstrapers
{
    public class GameEnterBootstraper : MonoBehaviour
    {
        private IGameStateMachine _gameStateMachine;

        [Inject]
        public void Construct(IGameStateMachine gameStateMachine)
            => _gameStateMachine = gameStateMachine;

        private void Start()
            => _gameStateMachine.Enter<BootstrapState>();
    }
}