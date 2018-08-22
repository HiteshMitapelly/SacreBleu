using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SacreBleu.Levels;

namespace SacreBleu.GameObjects
{
    class TestPlayer : GameObject
    {
        public float _speed;
        public float _drag;
        private Vector2 MovementDirection;
        private GameObject OverlappingObject;

        public TestPlayer(Vector2 position, SpriteBatch spriteBatch, Texture2D sprite, float speed, float drag)
            : base(position, spriteBatch, sprite)
        {
            _speed = speed;
            _drag = drag;

            _tag = "Player";
        }

        public void Update(GameTime gameTime)
        {
            PollInput();

            //simulate drag
            MovementDirection -= MovementDirection * _drag;

            //move player
            Move(gameTime);
        }

        private void PollInput()
        {
            KeyboardState input = Keyboard.GetState();
            if (input.IsKeyDown(Keys.Left)) { MovementDirection += new Vector2(-1, 0); }
            if (input.IsKeyDown(Keys.Right)) { MovementDirection += new Vector2(1, 0); }
            if (input.IsKeyDown(Keys.Up)) { MovementDirection += new Vector2(0, -1); }
            if(input.IsKeyDown(Keys.Down)) { MovementDirection += new Vector2(0, 1); }
        }

        private void Move(GameTime gameTime)
        {
            Vector2 previousPosition = _position;
            _position += MovementDirection * _speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds / 15;

            OverlappingObject = LevelTest.instance.baseLevel.RectangleOverlapping(_bounds);
            if (OverlappingObject != null)
            {
                if (OverlappingObject._tag.Equals("Obstacle"))
                    _position = previousPosition;
                else if (OverlappingObject._tag.Equals("Hazard"))
                    Death();
            }
        }

        private void Death()
        {
            //kill the player
        }

        public override void Draw()
        {
            base.Draw();

            if (OverlappingObject != null && OverlappingObject._tag.Equals("Obstacle"))
            {
                _spriteBatch.DrawString(LevelTest.instance._levelFont, "overlapping", new Vector2(_position.X, _position.Y + 10), Color.Black);
            }
        }
    }
}
