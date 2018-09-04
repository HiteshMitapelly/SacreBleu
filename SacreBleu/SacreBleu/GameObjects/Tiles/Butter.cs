using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SacreBleu.GameObjects.Tiles
{
    class Butter : GameObject
    {
        public Butter(Vector2 position, Texture2D sprite)
            : base(position, sprite)
        {
            _tag = "Butter";
            _isTrigger = true;
        }
    }
}
