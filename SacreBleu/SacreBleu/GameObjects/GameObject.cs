using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SacreBleu.GameObjects
{
    public class GameObject
    {
        //id properties
        public string _tag;

        //transform properties
        public Vector2 _position;
        public float _rotation;

        //graphics properties
        public Texture2D _sprite;
        public Color _tint;
        public float _scale;

        //collision properties
        public Rectangle _bounds;
        public float _boundsOriginX;
        public float _boundsOriginY;
        public float _boundsWidth;
        public float _boundsHeight;
        public bool _isTrigger;
        public int layerDepth;

        public GameObject(Vector2 position, Texture2D sprite)
        {
            _tag = "";
            _position = position;
            _rotation = 0f;
            _sprite = sprite;
            _tint = Color.White;
            _scale = SacreBleuGame._instance.baseScale;
            _boundsOriginX = 0;
            _boundsOriginY = 0;
            _boundsWidth = _sprite.Width;
            _boundsHeight = _sprite.Height;
            _isTrigger = true;
        }

        public Rectangle GetBounds()
        {
            return new Rectangle((int)_position.X + (int)_boundsOriginX, (int)_position.Y + (int)_boundsOriginY, (int)(_boundsWidth * _scale), (int)(_boundsHeight * _scale));
        }

        public virtual void Draw()
        {
            SacreBleuGame._instance._spriteBatch.Draw(_sprite, _position, null, _tint, _rotation, Vector2.Zero, _scale, SpriteEffects.None, layerDepth);
        }
    }
}
