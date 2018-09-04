using Microsoft.Xna.Framework;
using SacreBleu.GameObjects;
using SacreBleu.GameObjects.Tiles;

namespace SacreBleu.Managers
{
    class TileManager
    {
        public static TileManager Instance;

        public TileManager()
        {
            Instance = this;
        }

        public GameObject CreateTile(int tile, Vector2 position)
        {
            switch (tile)
            {
                case 0: // burner
                    return new Hazard(position, SacreBleuGame._instance.burnerTexture);
                case 19: // butter
                    return new Butter(position, SacreBleuGame._instance.butterBeforeTexture);
                case 3: // counter dark
                    return new GameObject(position, SacreBleuGame._instance.counterDarkTexture);
                case 4: // counter light
                    return new GameObject(position, SacreBleuGame._instance.counterLightTexture);
                case 20: // cutting board
                    return new CuttingBoard(position, SacreBleuGame._instance.cuttingBoardTexture);
                case 23: // flour
                    return new Flour(position, SacreBleuGame._instance.flourTexture);
                case 21: // pan
                    return new Goal(position, SacreBleuGame._instance.goalTexture);
                case 24: // sink
                    return new Hazard(position, SacreBleuGame._instance.sinkTexture);
                case 10: // black tile
                    return new Obstacle(position, SacreBleuGame._instance.blackTileTexture);
                case 11: // white tile
                    return new Obstacle(position, SacreBleuGame._instance.whiteTileTexture);
                case 14: // small black tile
                    return new Obstacle(position, SacreBleuGame._instance.smallBlackTileTexture);
                case 15:
                    return new Obstacle(position, SacreBleuGame._instance.smallWhiteTileTexture);
                default:
                    return null;
            }
        }
    }
}
