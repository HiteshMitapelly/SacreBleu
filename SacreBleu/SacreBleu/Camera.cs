using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SacreBleu.GameObjects;

namespace SacreBleu
{
	public class Camera 
	{
		public static Camera _instance;
		public Matrix Transform { get; private set; }

		public Camera() {
			_instance = this;
		}
		public void Follow(GameObject target)  // target is frog
		{
			Vector2 minworldPosition = Vector2.Transform(new Vector2(0,SacreBleuGame._instance._screenHeight), Matrix.Invert(SacreBleuGame._instance._camera.Transform));
			Vector2 maxworldPosition = Vector2.Transform(new Vector2(0, 0), Matrix.Invert(SacreBleuGame._instance._camera.Transform));
		

			var position = Matrix.CreateTranslation(
			  -SacreBleuGame._instance._screenWidth / 2,
			 -target._position.Y - (target._bounds.Height / 2)
			 ,
			  0); //  MathHelper.Clamp(-target._position.Y - (target._bounds.Height / 2), 0f, 2200f) -target._position.Y - (target._bounds.Height / 2)

			var offset = Matrix.CreateTranslation(
				SacreBleuGame._instance._screenWidth / 2,
				SacreBleuGame._instance._screenHeight / 2,
				0);

			Transform = position * offset;
			MathHelper.Clamp(Transform.Up.Y, SacreBleuGame._instance._screenHeight / 2, 2200f);
		}
	}
}