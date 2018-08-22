using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SacreBleu.GameObjects
{
	class Frog : GameObject
	{
		//Vector2 frogPosition;
		Vector2 frogVelocity;
		Vector2 initPosition, finalPosition;
		float Xoffset, Yoffset;
		//Rectangle bbox;
		bool dragging, released;

		Rectangle r;
		Vector2 v;
		float angle;

		public Frog(Vector2 position, Texture2D sprite) : base(position, sprite)
		{
			_tag = "frog";

			Start();
		}

        public void Start()
		{
			initPosition = _position;
			dragging = false;
			released = false;
		}

        public void Update(GameTime gameTime)
		{
            MouseState mouseState = Mouse.GetState();

            bool inRange = _bounds.Contains(new Point((int)mouseState.X, (int)mouseState.Y));

            if (SacreBleuGame.currentState == Gamestates.READY)  // This is to drag frog
			{
				if ((mouseState.LeftButton == ButtonState.Pressed) && inRange)
				{
					dragging = true;
					Vector2 end = new Vector2(mouseState.X, mouseState.Y);
					dragLine(initPosition, end, Color.Black, 1);
				}

				if ((mouseState.LeftButton == ButtonState.Released) && dragging)
				{
					finalPosition = new Vector2(mouseState.X, mouseState.Y);
					Xoffset = finalPosition.X - initPosition.X;
					Yoffset = finalPosition.Y - initPosition.Y;
					Xoffset *= 3;
					Yoffset *= 3;
					frogVelocity = new Vector2(Xoffset, Yoffset);

					SacreBleuGame.currentState = Gamestates.RELEASED;
					dragging = false;
				}
			}

			if ((SacreBleuGame.currentState == Gamestates.RELEASED) && !dragging) // This is to release frog
			{

				released = true;
				if (Yoffset < 0)
					released = false;
				if (released)
					releaseFrog(gameTime);
			}
		}

		void dragLine(Vector2 begin, Vector2 end, Color color, int width = 1)
		{
			r = new Rectangle((int)begin.X, (int)begin.Y, (int)(end - begin).Length() + width, width);
			v = Vector2.Normalize(begin - end);
			angle = (float)Math.Acos(Vector2.Dot(v, -Vector2.UnitX));
			if (begin.Y > end.Y)
			{
				angle = MathHelper.TwoPi - angle;
			}
		}

		void releaseFrog(GameTime gameTime)
		{
			_position.X -= frogVelocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds;
			_position.Y -= frogVelocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;
			Xoffset -= 2;
			Yoffset -= 4;
		}

		public void draw(SpriteBatch spritebatch)
		{
			base.Draw();
            SacreBleuGame._instance._spriteBatch.Draw(_sprite, r, null, Color.White, angle, Vector2.Zero, SpriteEffects.None, 0);
        }
	}
}
