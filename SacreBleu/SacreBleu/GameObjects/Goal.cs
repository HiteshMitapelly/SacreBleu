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
            _boundsOriginX = _sprite.Width / 4;
            _boundsOriginY = _sprite.Height / 4;
            _boundsWidth = _sprite.Width / 2;
            _boundsHeight = _sprite.Height / 2;
        }
    }
}
