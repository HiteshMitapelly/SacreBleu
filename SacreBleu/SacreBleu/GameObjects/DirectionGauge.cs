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
	class DirectionGauge : GameObject
	{
		Vector2 initBottomPosition;
		KeyboardState keyboardState,previousKeyboardState;
		
		public float _angle;
		float maxAngle;
		float angleChange;

		public DirectionGauge(Vector2 position,Texture2D sprite) :base(position,sprite)
		{			
			_angle = 0f;
		
			maxAngle = (float)Math.PI / 2.05f;
			angleChange = 0.025f;
			initBottomPosition = new Vector2(_bounds.X, _bounds.Y + _bounds.Height);
		}

		public void Update(GameTime gameTime)
		{
			keyboardState = Keyboard.GetState();	
			
			if (keyboardState.IsKeyDown(Keys.D) && !previousKeyboardState.IsKeyDown(Keys.Right))
			{				
				if(_angle <= maxAngle)
					_angle += angleChange;				
			}
			if (keyboardState.IsKeyDown(Keys.A) && !previousKeyboardState.IsKeyDown(Keys.Left))
			{
				if(_angle >= -maxAngle)
					_angle -= angleChange;
			}
			
			previousKeyboardState = keyboardState;

            _position = new Vector2(LevelManager._instance.currentLevel._frog._position.X + LevelManager._instance.currentLevel._frog._sprite.Width / 2, LevelManager._instance.currentLevel._frog._position.Y + LevelManager._instance.currentLevel._frog._sprite.Height / 2);
		}

		public override void  Draw()
		{			
			var origin = new Vector2(_sprite.Width / 2 , _sprite.Height );
			SacreBleuGame._instance._spriteBatch.Draw(_sprite, _position, null, Color.White, _angle, origin, 1f, SpriteEffects.None, 0f);
		}
	}
}
