using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SacreBleu.GameObjects
{
    class Goal : GameObject
    {
        public Goal(Vector2 position, Texture2D sprite)
            : base(position, sprite)
        {
            _tag = "Goal";
            _isTrigger = true;
            int quarterWidth = _sprite.Width / 4;
            _bounds = new Rectangle((int)_position.X + quarterWidth, (int)_position.Y + quarterWidth, quarterWidth * 2, quarterWidth * 2);
        }
    }
}
