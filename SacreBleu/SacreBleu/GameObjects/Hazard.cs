using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SacreBleu.GameObjects
{
    class Hazard : GameObject
    {
        public Hazard(Vector2 position, Texture2D sprite)
            : base(position, sprite)
        {
            _tag = "Hazard";
            _isTrigger = true;
            _tint = Color.Red;
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
