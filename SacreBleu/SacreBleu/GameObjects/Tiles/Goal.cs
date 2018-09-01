using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SacreBleu.Managers;

namespace SacreBleu.GameObjects
{
    class Goal : GameObject
    {
        public Goal(Vector2 position, Texture2D sprite)
            : base(position, sprite)
        {
            _tag = "Goal";
            _isTrigger = true;
        }
    }
}
