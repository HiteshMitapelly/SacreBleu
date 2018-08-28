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
        public int _boundsOriginX;
        public int _boundsOriginY;
        public int _boundsWidth;
        public int _boundsHeight;
        public bool _isTrigger;

        public GameObject(Vector2 position, Texture2D sprite)
        {
            _tag = "";
            _position = position;
            _rotation = 0f;
            _sprite = sprite;
            _tint = Color.White;
            _scale = 1f;
            _boundsOriginX = 0;
            _boundsOriginY = 0;
            _boundsWidth = _sprite.Width;
            _boundsHeight = _sprite.Height;
            _isTrigger = false;
        }

        public Rectangle GetBounds()
        {
            return new Rectangle((int)_position.X + _boundsOriginX, (int)_position.Y + _boundsOriginY, _boundsWidth, _boundsHeight);
        }

        public virtual void Draw()
        {
            //calculate scale multiplier
            float scale = 32f / _sprite.Width;

            SacreBleuGame._instance._spriteBatch.Draw(_sprite, _position, null, _tint, _rotation, Vector2.Zero, _scale, SpriteEffects.None, 0f);
        }
    }
}
