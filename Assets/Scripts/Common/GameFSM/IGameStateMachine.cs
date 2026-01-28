namespace Common.GameFSM
{
    public interface IGameStateMachine
    {
        public void Enter<T>() where T: IGameState;
    }
}
