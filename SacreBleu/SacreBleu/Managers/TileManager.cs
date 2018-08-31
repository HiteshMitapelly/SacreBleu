namespace SacreBleu.Managers
{
    class TileManager
    {
        public static TileManager Instance;

        public enum Tiles
        {
            BACKGROUND,
            BOUNDARY,
            CUTTING_BOARD,
            FRYING_PAN,
            BURNER
        }

        public TileManager()
        {
            Instance = this;
        }
    }
}
