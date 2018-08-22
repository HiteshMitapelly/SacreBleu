using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace SacreBleu
{
	public class Frog
	{
		float testVariable;
		Vector2 frogPosition;
		Vector2 frogVelocity;
		Vector2 initPosition, finalPosition;
		float Xoffset, Yoffset;
		Rectangle bbox;
		bool dragging,released;

		public Vector2 position;
		public Texture2D frogSprite;


		/// <summary>
		/// Initializing values for frog class.
		/// This is called from LoadContent of Game1.cs
		/// </summary>
		public void Start() {
			initPosition = frogPosition;
			dragging = false;
			released = false;
			
		}

		/// <summary>
		/// Constructor for frog class
		/// </summary>
		/// <param name="sprite"></param>
		/// <param name="position"></param>
		public Frog(Texture2D sprite, Vector2 position)
		{
			frogSprite = sprite;
			frogPosition = position;
			bbox = new Rectangle((int)position.X - sprite.Width / 2, (int)position.Y - sprite.Height / 2, sprite.Width, sprite.Height);
		}


		public void Update(GameTime gameTime)
		{

			bbox = new Rectangle((int)frogPosition.X - frogSprite.Width / 2, (int)frogPosition.Y - frogSprite.Height / 2, frogSprite.Width, frogSprite.Height);

			MouseState mouseState = Mouse.GetState();


			bool inRange = bbox.Contains(new Point((int)mouseState.X, (int)mouseState.Y));
		     

			if (SacreBleuGame.currentState == Gamestates.READY)  // This is to drag frog
			{
				
				if ((mouseState.LeftButton == ButtonState.Pressed) && inRange)
				{

					dragging = true;
					
					dragFrog(new Vector2(mouseState.X, mouseState.Y));
					
				}


				if ((mouseState.LeftButton == ButtonState.Released )&& dragging)
				{
					finalPosition = frogPosition;
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

		void dragFrog(Vector2 position)
		{
			if (dragging)
			{

				frogPosition = position ;

			}
		}

		void releaseFrog(GameTime gameTime) {
			frogPosition.X -= frogVelocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds;
			frogPosition.Y -= frogVelocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;
			Xoffset -= 2;
			Yoffset -= 4;

		}

		 
		public void draw(SpriteBatch spritebatch)
		{
			spritebatch.Draw(frogSprite, frogPosition, Color.White);

		}


	}

}
