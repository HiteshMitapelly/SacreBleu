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
	class PowerBar : GameObject
	{
		public static PowerBar _instance;

		Vector2 direction;
		Texture2D powerBarTexture, powerGaugeTexture;
		float powerBarHeight;
		float powerMagnitude;
		int heightFactor;
		public bool startBar;
		
		public PowerBar(Vector2 position,Texture2D healthBar,Texture2D healthGauge) : base(position,healthBar)
		{
			_instance = this;
			powerBarTexture = healthBar;
			powerGaugeTexture = healthGauge;
			powerBarHeight = powerBarTexture.Height;
			heightFactor = -1;
			_tag = "power bar";
			startBar = false;
		}

		public  void Update(GameTime gameTime) {

			if (startBar)
			{
				powerBarHeight += heightFactor;

				if ((powerBarHeight > (powerBarTexture.Height)) || (powerBarHeight < 0))
					heightFactor = -heightFactor;

			}


		}

		public void setpower() {
			
			powerMagnitude = (float)Math.Round((1 - Math.Abs(powerBarHeight/_sprite.Height)),1);
			
			Debug.WriteLine("power generated " + powerMagnitude);
			float angle = DirectionGauge._instance._angle;
			double degrees = MathHelper.ToDegrees(angle);

			degrees = 90 - degrees;
			if (degrees < 90)
			{
				direction = new Vector2(1f, -(float)Math.Tan(degrees));
			}
			if (degrees > 90)
			{
				direction = new Vector2(-1f, (float)Math.Tan(180 - degrees));
			}
			if (degrees == 90)
			{
				direction = new Vector2(0, -1);
			}
			direction.Normalize();//direction = new Vector2(-(float)Math.Cos(degrees), (float)Math.Sin(degrees));
			
			Debug.WriteLine(direction);

			Frog._instance.SetVelocity(powerMagnitude * direction * Frog._instance._maxVelocity);

		}
		public override void Draw()
		{
			
			
			base.Draw();
			Vector2 adjustedRectanglePos = new Vector2(_position.X, _position.Y);
			SacreBleuGame._instance._spriteBatch.Draw(powerBarTexture, adjustedRectanglePos, new Rectangle((int)_position.X, (int)_position.Y, _sprite.Width, (int) powerBarHeight), Color.Black);
		}

	}
}
