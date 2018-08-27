namespace SacreBleu.Managers
{
    class GameManager
    {
        public static GameManager _instance;

        public enum GameStates { PAUSED, READY, RELEASED }
        public GameStates _currentState;

        public GameManager()
        {
            _instance = this;

            _currentState = GameStates.READY;
        }
    }
}
