using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SacreBleu.Levels;

namespace SacreBleu.GameObjects
{
    class GameObject
    {
        //id properties
        public string _tag;

        //transform properties
        public Vector2 _position;
        public float _rotation;

        //graphics properties
        public SpriteBatch _spriteBatch;
        public Texture2D _sprite;
        public float _scale;

        //collision properties
        public Rectangle _bounds;
        public bool _isTrigger;

        public GameObject(Vector2 position, SpriteBatch spriteBatch, Texture2D sprite)
        {
            _tag = "";
            _position = position;
            _rotation = 0f;
            _spriteBatch = spriteBatch;
            _sprite = sprite;
            _scale = 1f;
            _bounds = new Rectangle((int)_position.X, (int)_position.Y, _sprite.Width, _sprite.Height);
            _isTrigger = false;
        }

        public virtual void Draw()
        {
            _spriteBatch.Draw(_sprite, _position, null, Color.White, _rotation, new Vector2(_sprite.Width / 2, _sprite.Height / 2), _scale, SpriteEffects.None, 0f);

            _spriteBatch.DrawString(LevelTest.instance._levelFont, _tag, _position, Color.Black);
        }
    }
}
