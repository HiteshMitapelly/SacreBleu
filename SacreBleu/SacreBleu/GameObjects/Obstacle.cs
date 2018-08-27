using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SacreBleu.GameObjects
{
    class Obstacle : GameObject
    {
        public Obstacle(Vector2 position, Texture2D sprite)
            : base(position, sprite)
        {
            _tag = "Obstacle";
            _tint = Color.Black;
        }
    }
}
