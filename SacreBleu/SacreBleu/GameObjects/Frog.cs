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
	class Frog : MoveableGameObject
	{
		public static Frog _instance;

        MouseState mouseState;
        Vector2 _mouseInitPosition, _mouseFinalPosition;
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
			_instance = this;

            _mouseInitPosition = _position;
            dragging = false;
        }

        public override void Update(GameTime gameTime)
		{
            Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);

            worldPosition = Vector2.Transform(mousePosition, Matrix.Invert(Camera._instance.Transform));

            mouseState = Mouse.GetState();
			inRange = GetBounds().Contains(new Point((int)worldPosition.X + (_sprite.Width / 2), (int)worldPosition.Y + (_sprite.Height / 2)));

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
				Vector2 end = new Vector2(worldPosition.X, worldPosition.Y);
				DragLine(_mouseInitPosition, end, Color.White, 1);
			}

			if (((mouseState.LeftButton == ButtonState.Released)&&(oldMouseState.LeftButton == ButtonState.Pressed))&& dragging)  // dragged and released condition
			{
                Vector2 velocity = Vector2.Zero;

				dragging = false;
				_mouseFinalPosition = new Vector2(worldPosition.X, worldPosition.Y);
                velocity = new Vector2(_mouseFinalPosition.X - _mouseInitPosition.X, _mouseFinalPosition.Y - _mouseInitPosition.Y);
                //releaseFrog(gameTime);
                SetVelocity(-velocity * 0.5f);
				SacreBleuGame._instance.currentState = Gamestates.RELEASED;
			}

            oldMouseState = mouseState;

            base.Update(gameTime);

			

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

			
		}

		void DragLine(Vector2 begin, Vector2 currentPosition, Color color, int width = 1)
		{
			_line = new Rectangle((int)begin.X, (int)begin.Y, (int)(currentPosition - begin).Length() + width, width);
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
            if(dragging)
                SacreBleuGame._instance._spriteBatch.Draw(_sprite, _line, null, Color.White, angle, Vector2.Zero, SpriteEffects.None, 0);

            SacreBleuGame._instance._spriteBatch.DrawString(SacreBleuGame._instance._levelFont, mouseState.X + " : " + mouseState.Y, new Vector2(worldPosition.X, worldPosition.Y), Color.Black);

            if (inRange)
            {
                SacreBleuGame._instance._spriteBatch.DrawString(SacreBleuGame._instance._levelFont, "In Range", new Vector2(_position.X, _position.Y + 25), Color.Black);
            }
        }
	}
}
