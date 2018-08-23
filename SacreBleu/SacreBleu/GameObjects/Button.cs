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
	class Button : GameObject
	{
		private Vector2 worldPosition;
		MouseState mouseState, oldMouseState;
		bool inRange;

		public Button(Vector2 position, Texture2D sprite) : base(position, sprite) {

			_tag = "button";

		}

		public void Update(GameTime gameTime) {
			Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);

			worldPosition = Vector2.Transform(mousePosition, Matrix.Invert(Camera._instance.Transform));

			mouseState = Mouse.GetState();
			
			inRange = GetBounds().Contains(new Point((int)worldPosition.X + (_sprite.Width / 2), (int)worldPosition.Y + (_sprite.Height / 2)));

			if (inRange) {
				
				if ((mouseState.LeftButton == ButtonState.Pressed) && (oldMouseState.LeftButton == ButtonState.Pressed)) {

					PowerBar._instance.startBar = true;

				}

				if ((mouseState.LeftButton == ButtonState.Released) && (oldMouseState.LeftButton == ButtonState.Pressed))
				{
					PowerBar._instance.startBar = false;
					PowerBar._instance.setpower();

				}

			}



			oldMouseState = mouseState;
		}
		public override void Draw()
		{
						
			base.Draw();
			
		}
	}
}
