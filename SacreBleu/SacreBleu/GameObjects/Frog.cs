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
		
		Vector2 frogVelocity;
		Vector2 initPosition, finalPosition;
		
		bool dragging;
		MouseState oldMouseState;
		Rectangle r;
		Vector2 v;
		float angle;

		//float Xoffset, Yoffset,counter;
		//bool released;
		//Rectangle bbox;
		//Vector2 frogPosition;


		public Frog(Vector2 position, Texture2D sprite) : base(position, sprite)
		{
			_tag = "frog";

			Start();
		}

        public void Start()
		{
			initPosition = _position;
			dragging = false;
			//released = false;
			
		}

        public void Update(GameTime gameTime)
		{
			MouseState mouseState = Mouse.GetState();
			bool inRange = _bounds.Contains(new Point((int)mouseState.X, (int)mouseState.Y));

			

				if (inRange) { 
				if ((oldMouseState.LeftButton == ButtonState.Released) && (mouseState.LeftButton == ButtonState.Pressed)) //player started to drag condition
				{
					

					dragging = true;
						initPosition = new Vector2(mouseState.X, mouseState.Y);

					
				}
			}

			if (((mouseState.LeftButton == ButtonState.Pressed) && (oldMouseState.LeftButton == ButtonState.Pressed)) && dragging) // in dragging state condition
			{
				Vector2 end = new Vector2(mouseState.X, mouseState.Y);
				dragLine(initPosition, end, Color.White, 1);
			}

			if (((mouseState.LeftButton == ButtonState.Released)&&(oldMouseState.LeftButton == ButtonState.Pressed))&& dragging)  // dragged and released condition
			{
				
				dragging = false;
				finalPosition = new Vector2(mouseState.X, mouseState.Y);
				frogVelocity = new Vector2(finalPosition.X - initPosition.X, finalPosition.Y - initPosition.Y);
				releaseFrog(gameTime);
				SacreBleuGame._instance.currentState = Gamestates.RELEASED;
			}

			

			

			/* old code (pls dont delete this. I(Hitesh) might need for future reference)

				if (SacreBleuGame._instance.currentState == Gamestates.READY)  // This is to drag frog
			{
				if ((mouseState.LeftButton == ButtonState.Pressed) && inRange)
				{
					dragging = true;
					Vector2 end = new Vector2(mouseState.X, mouseState.Y);
					dragLine(initPosition, end, Color.Black, 1);
				}

				if ((mouseState.LeftButton == ButtonState.Released) && dragging)
				{
					Debug.WriteLine("mouse" + mouseState.Y);
					finalPosition = new Vector2(mouseState.X, mouseState.Y);
					Debug.WriteLine("final pos " + finalPosition);
					Xoffset = finalPosition.X - initPosition.X;
					Yoffset = finalPosition.Y - initPosition.Y;
					Xoffset *= 3;
					Yoffset *= 3;
					counter = Math.Abs(Yoffset);
					frogVelocity = new Vector2(Xoffset, Yoffset);
					

					SacreBleuGame._instance.currentState = Gamestates.RELEASED;
					dragging = false;
				}
			}

			if ((SacreBleuGame._instance.currentState == Gamestates.RELEASED) && !dragging) // This is to release frog
			{

				released = true;
				if (counter < 0)
				{
					released = false;
					SacreBleuGame._instance.currentState = Gamestates.READY;
					initPosition = _position;
					
					Debug.WriteLine("init " + initPosition);
					Xoffset = 0;
					Yoffset = 0;
				}
				if (released)
					releaseFrog(gameTime);
			}*/

			oldMouseState = mouseState;
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
			float timer = Math.Abs( frogVelocity.Y * 10f);
			while (timer > 1)   // frog flight loop
			{
				_position -= frogVelocity * 10 * (float)gameTime.ElapsedGameTime.TotalSeconds;
				timer /= 2f;
			}
			initPosition = _position;
			SacreBleuGame._instance.currentState = Gamestates.READY;

			/*Debug.WriteLine(counter);
			_position.X -= frogVelocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds;
			_position.Y -= frogVelocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;
			//Xoffset -= 2;
			counter -= 4;*/
		}

		public void draw(SpriteBatch spritebatch)
		{
			base.Draw();
            SacreBleuGame._instance._spriteBatch.Draw(_sprite, r, null, Color.White, angle, Vector2.Zero, SpriteEffects.None, 0);
        }
	}
}
