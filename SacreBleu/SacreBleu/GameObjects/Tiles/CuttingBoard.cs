using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SacreBleu.GameObjects.Tiles
{
    class CuttingBoard : GameObject
    {
        public CuttingBoard(Vector2 position, Texture2D sprite)
            : base(position, sprite)
        {
            _tag = "Cutting Board";
        }
    }
}
