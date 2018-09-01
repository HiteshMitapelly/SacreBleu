using Microsoft.Xna.Framework;
using SacreBleu.GameObjects;
using SacreBleu.GameObjects.Tiles;
using System.Diagnostics;

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
                case 1:
                    GameObject counterTile = new GameObject(position, SacreBleuGame._instance.counterTexture);
                    return counterTile;
                case 2:
                    Obstacle obstacle = new Obstacle(position, SacreBleuGame._instance.blackTileTexture);
                    return obstacle;
                case 3:
                    CuttingBoard cuttingBoard = new CuttingBoard(position, SacreBleuGame._instance.cuttingBoardTexture);
                    return cuttingBoard;
                case 4:
                    Goal goal = new Goal(position, SacreBleuGame._instance.goalTexture);
                    return goal;
                case 5:
                    Burner burner = new Burner(position, SacreBleuGame._instance.burnerTexture);
                    return burner;
                default:
                    return null;
            }
        }
    }
}
