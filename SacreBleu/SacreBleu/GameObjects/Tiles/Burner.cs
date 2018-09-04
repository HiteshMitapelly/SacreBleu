using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SacreBleu.GameObjects.Tiles
{
    class Burner : Hazard
    {
        public Burner(Vector2 position, Texture2D sprite)
            : base(position, sprite)
        {

        }

        public override void Draw()
        {
            base.Draw();

            SacreBleuGame._instance._spriteBatch.Draw(SacreBleuGame._instance.fireTexture, _position, null, _tint, _rotation, Vector2.Zero, _scale, SpriteEffects.None, layerDepth);
        }
    }
}
