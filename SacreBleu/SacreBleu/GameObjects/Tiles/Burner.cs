using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SacreBleu.GameObjects.Tiles
{
    class Burner : GameObject
    {
        public Burner(Vector2 position, Texture2D sprite)
            : base(position, sprite)
        {
            _tag = "Burner";
        }
    }
}
