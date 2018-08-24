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
	class DirectionGauge :GameObject
	{

		public static DirectionGauge _instance;

		Vector2 initBottomPosition;
		KeyboardState keyboardState,previousKeyboardState;
		public Vector2 _direction;
		
		
		public float _angle;
		float maxAngle;
		float angleChange;
		public DirectionGauge(Vector2 position, Texture2D sprite) : base(position,sprite)
		{
			_instance = this;	
			_angle = 0f;
			maxAngle = 0.7f;
			angleChange = 0.1f;
			initBottomPosition = new Vector2(_bounds.X, _bounds.Y + _bounds.Height);
			Debug.WriteLine(initBottomPosition);

		}

		public void Update(GameTime gameTime)
		{
			keyboardState = Keyboard.GetState();
	
			
			if (keyboardState.IsKeyDown(Keys.Right) && !previousKeyboardState.IsKeyDown(Keys.Right))
			{
				
				if(_angle < maxAngle)
					_angle += angleChange;
				
			}
			if (keyboardState.IsKeyDown(Keys.Left) && !previousKeyboardState.IsKeyDown(Keys.Left))
			{
				if(_angle > -maxAngle)
					_angle -= angleChange;
			}
			
			previousKeyboardState = keyboardState;
		}

		public override void Draw()
		{
			
			var origin = new Vector2(_sprite.Width/2 , _sprite.Height );
			SacreBleuGame._instance._spriteBatch.Draw(_sprite, _position, null, Color.White, _angle,
			origin, 1f, SpriteEffects.None, 0f);
	


		}
	}
}
