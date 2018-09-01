using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SacreBleu.GameObjects;
using SacreBleu.Managers;

namespace SacreBleu.GameObjects
{
    class ButtonGameObject
    {

        //id properties
        public string _tag;

        //transform properties
        public Vector2 _position;
       
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

        //Button Properties
        public bool _isPressed;
        MouseState mouseState, oldMouseState;
        bool inRange;
        Vector2 mousePosition;

        public ButtonGameObject(Vector2 position, Texture2D sprite, String tag)
        {
            _tag = tag;
            _position = position;
            _isPressed = false;
            _sprite = sprite;
            _tint = Color.White;
            _scale = 1f;
            _boundsOriginX = 0;
            _boundsOriginY = 0;
            _boundsWidth = _sprite.Width;
            _boundsHeight = _sprite.Height;
            _bounds = GetBounds();
        }
        public Rectangle GetBounds()
        {
            return new Rectangle((int)_position.X + _boundsOriginX, (int)_position.Y + _boundsOriginY, _boundsWidth, _boundsHeight);
        }
        public virtual void Update(GameTime gameTime)
        {
            if(GameManager._instance._currentState == GameManager.GameStates.READY)
            {
                _isPressed = false;
            }
            mousePosition = new Vector2(mouseState.X, mouseState.Y);

            mouseState = Mouse.GetState();

            inRange = GetBounds().Contains(new Point((int)mousePosition.X, (int)mousePosition.Y));

            if (inRange)
            {

                if (mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
                {
                   
                    _isPressed = true;
                   
                }

            }
           
            oldMouseState = mouseState;


        }
        public ButtonState CurrentButtonState() {
            if (_isPressed)
                return ButtonState.Pressed;
            else
                return ButtonState.Released;
        }
        public virtual void Draw()
        {
            SacreBleuGame._instance._spriteBatch.Draw(_sprite, _position, null, _tint, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

    }
}
