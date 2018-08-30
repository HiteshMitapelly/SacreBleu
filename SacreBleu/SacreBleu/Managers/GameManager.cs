namespace SacreBleu.Managers
{
    class GameManager
    {
        public static GameManager _instance;

        public enum GameStates { IDLE,PAUSED,READY, WON }
        public GameStates _currentState;

        public GameManager()
        {
            _instance = this;

            _currentState = GameStates.IDLE;
        }
    }
}
