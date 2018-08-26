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
            _tint = Color.Green;

            _mouseInitPosition = _position;
            dragging = false;
        }

        public override void Update(GameTime gameTime)
		{
			
				Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);

				worldPosition = Vector2.Transform(mousePosition, Matrix.Invert(SacreBleuGame._instance._camera.Transform));

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
					Vector2 end = new Vector2(worldPosition.X, worldPosition.Y);
					DragLine(_mouseInitPosition, end, Color.White, 1);
				}

				if (((mouseState.LeftButton == ButtonState.Released) && (oldMouseState.LeftButton == ButtonState.Pressed)) && dragging)  // dragged and released condition
				{
					Vector2 velocity = Vector2.Zero;

					dragging = false;
					_mouseFinalPosition = new Vector2(worldPosition.X, worldPosition.Y);
					velocity = new Vector2(_mouseFinalPosition.X - _mouseInitPosition.X, _mouseFinalPosition.Y - _mouseInitPosition.Y);

					SetVelocity(-velocity * 0.25f);
					SacreBleuGame._instance.currentState = Gamestates.RELEASED;
				}

				oldMouseState = mouseState;
			
            base.Update(gameTime);

			

			

			
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
            if(dragging)
                SacreBleuGame._instance._spriteBatch.Draw(_sprite, _line, null, Color.White, angle, Vector2.Zero, SpriteEffects.None, 0);

            //SacreBleuGame._instance._spriteBatch.DrawString(SacreBleuGame._instance._levelFont, mouseState.X + " : " + mouseState.Y, new Vector2(worldPosition.X, worldPosition.Y), Color.Black);
        }
	}
}
