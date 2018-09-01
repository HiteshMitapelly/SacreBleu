using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;

namespace SacreBleu.GameObjects
{
    class Frog : MoveableGameObject
    {
        public enum States
        {
            IDLE,
            TRAVELLING,
            DEAD
        }

        public States _currentState;

        MouseState mouseState;
        Vector2 _mouseInitPosition, _mouseFinalPosition, _mousePosition;
        Vector2 worldPosition;


        public float _maxVelocity = 25f;
        bool dragging;
        MouseState oldMouseState;
        Rectangle _line;
        Vector2 _lineVector;
        float angle;


        bool inRange;

        public Frog(Vector2 position, Texture2D sprite, float drag, float bounce) : base(position, sprite, drag, bounce)
        {
            _tag = "Frog";
            _isTrigger = false;

            _mouseInitPosition = _position;
            dragging = false;

            _boundsOriginX = 2;
            _boundsOriginY = 2;
            _boundsWidth = _sprite.Width - 2;
            _boundsHeight = _sprite.Height - 2;

            _scale *= 2;

            _currentState = States.IDLE;
        }

        public override void Update(GameTime gameTime)
        {
            if (_velocity.Length() > 0f && _currentState != States.TRAVELLING)
                _currentState = States.TRAVELLING;
            else if (_velocity.Length() == 0f && _currentState == States.TRAVELLING)
                _currentState = States.IDLE;

            _mousePosition = new Vector2(mouseState.X, mouseState.Y);

            worldPosition = Vector2.Transform(_mousePosition, Matrix.Invert(SacreBleuGame._instance._camera.Transform));

            mouseState = Mouse.GetState();
            inRange = GetBounds().Contains(new Point((int)worldPosition.X, (int)worldPosition.Y));

            if (inRange)
            {
                if ((oldMouseState.LeftButton == ButtonState.Released) && (mouseState.LeftButton == ButtonState.Pressed)) //player started to drag condition
                {
                    dragging = true;
                    _mouseInitPosition = new Vector2(_position.X, _position.Y);
                }
            }

            if (((mouseState.LeftButton == ButtonState.Pressed) && (oldMouseState.LeftButton == ButtonState.Pressed)) && dragging) // in dragging state condition
            {
                _mouseFinalPosition = new Vector2(worldPosition.X, worldPosition.Y);
                DragLine(_mouseInitPosition, _mouseFinalPosition, Color.White, 1);
            }

            if (((mouseState.LeftButton == ButtonState.Released) && (oldMouseState.LeftButton == ButtonState.Pressed)) && dragging)  // dragged and released condition
            {
                _velocity = Vector2.Zero;

                dragging = false;
                _mouseFinalPosition = new Vector2(worldPosition.X, worldPosition.Y);
                _velocity = new Vector2(_mouseFinalPosition.X - _mouseInitPosition.X, _mouseFinalPosition.Y - _mouseInitPosition.Y);

                SetVelocity(-_velocity * 0.25f);
                // GameManager._instance._currentState = GameManager.GameStates.READY;
            }

            oldMouseState = mouseState;

            base.Update(gameTime);
        }

        public void Death(Vector2 respawnPosition)
        {
            _currentState = States.DEAD;
            _velocity = Vector2.Zero;
            _position = respawnPosition;
            _currentState = States.IDLE;
        }

        void DragLine(Vector2 begin, Vector2 currentPosition, Color color, int width = 1)
        {
            _line = new Rectangle((int)begin.X + _sprite.Width / 2, (int)begin.Y + _sprite.Height / 2, (int)(currentPosition - begin).Length() + width, width);
            _lineVector = Vector2.Normalize(begin - currentPosition);
            angle = (float)Math.Acos(Vector2.Dot(_lineVector, -Vector2.UnitX));
            if (begin.Y > currentPosition.Y)
            {
                angle = MathHelper.TwoPi - angle;
            }
        }

        public override void Draw()
        {
            base.Draw();
            if (dragging)
                SacreBleuGame._instance._spriteBatch.Draw(_sprite, _line, null, Color.White, angle, Vector2.Zero, SpriteEffects.None, 0);
        }
    }
}
