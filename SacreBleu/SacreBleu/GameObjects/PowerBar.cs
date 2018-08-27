using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SacreBleu.Levels;

namespace SacreBleu.GameObjects
{
	class PowerBar : GameObject
	{
		Vector2 direction;
		Texture2D powerBarTexture, powerGaugeTexture;
		float powerBarHeight;
		float powerMagnitude;
		int heightFactor;
		public bool startBar;
		
		public PowerBar(Vector2 position,Texture2D healthBar,Texture2D healthGauge) : base(position,healthBar)
		{
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
			direction = Vector2.Zero;
            LevelManager._instance.currentLevel._frog.SetVelocity(powerMagnitude * direction * LevelManager._instance.currentLevel._frog._maxVelocity);			

			float angle = LevelManager._instance.currentLevel._directionGauge._angle;
			float degrees = MathHelper.ToDegrees(angle);

			degrees = 90 - degrees;			

			float tanValue;
			float radians = MathHelper.ToRadians(degrees);
			tanValue = (float)Math.Tan(radians);
			
			if (degrees < 90)
			{
				direction.X = (float)Math.Sqrt(1 / 1 + Math.Pow(tanValue, 2));
				direction.Y = -direction.X * tanValue;				
			}
			if (degrees > 90)
			{
				direction.X = -(float)Math.Sqrt(1 / 1 + Math.Pow(tanValue, 2));
				direction.Y = -direction.X * tanValue;				
			}
			if (degrees == 90)
			{
				direction = new Vector2(0, -1);
			}
		
			direction.Normalize();

            LevelManager._instance.currentLevel._frog.SetVelocity(powerMagnitude * direction * LevelManager._instance.currentLevel._frog._maxVelocity);
		}
		public override void Draw()
		{			
			SacreBleuGame._instance._spriteBatch.Draw(powerGaugeTexture, _position, new Rectangle((int)_position.X, (int)_position.Y, _sprite.Width,_sprite.Height ), Color.White);
			SacreBleuGame._instance._spriteBatch.Draw(powerBarTexture, _position, new Rectangle((int)_position.X, (int)_position.Y, _sprite.Width, (int) powerBarHeight), Color.Black);
		}

	}
}
