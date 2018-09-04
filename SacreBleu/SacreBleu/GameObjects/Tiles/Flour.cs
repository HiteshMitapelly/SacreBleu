using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SacreBleu.GameObjects.Tiles
{
    class Flour : GameObject
    {
        public Flour(Vector2 position, Texture2D sprite)
            : base(position, sprite)
        {
            _tag = "Flour";
            _isTrigger = true;
        }
    }
}
