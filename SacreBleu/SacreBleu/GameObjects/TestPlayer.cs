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
    class TestPlayer : MoveableGameObject
    {
        public TestPlayer(Vector2 position, Texture2D sprite, float speed, float drag)
            : base(position, sprite, drag)
        {
            _tag = "Player";
        }

        public override void Update(GameTime gameTime)
        {
            PollInput();

            base.Update(gameTime);
        }

        private void PollInput()
        {
            KeyboardState input = Keyboard.GetState();
            if (input.IsKeyDown(Keys.Left)) { _velocity += new Vector2(-1, 0); }
            if (input.IsKeyDown(Keys.Right)) { _velocity += new Vector2(1, 0); }
            if (input.IsKeyDown(Keys.Up)) { _velocity += new Vector2(0, -1); }
            if (input.IsKeyDown(Keys.Down)) { _velocity += new Vector2(0, 1); }
        }

        private void Death()
        {
            //kill the player
        }
    }
}
