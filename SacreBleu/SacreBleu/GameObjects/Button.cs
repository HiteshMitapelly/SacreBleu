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
		public static Button _instance;

		private Vector2 worldPosition;
		MouseState mouseState, oldMouseState;
		bool inRange;
		Vector2 mousePosition;

		public Button(Vector2 position, Texture2D sprite) : base(position, sprite)
		{
			_instance = this;
			_tag = "button";

		}

		public void Update(GameTime gameTime) {
			mousePosition = new Vector2(mouseState.X, mouseState.Y);

			//worldPosition = Vector2.Transform(mousePosition, Matrix.Invert(SacreBleuGame._instance._camera.Transform));

			mouseState = Mouse.GetState();
			
			inRange = GetBounds().Contains(new Point((int)mousePosition.X, (int)mousePosition.Y));

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
