using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SacreBleu.GameObjects
{
    class Hazard : GameObject
    {
        public Hazard(Vector2 position, Texture2D sprite)
            : base(position, sprite)
        {
            _tag = "Hazard";
            _isTrigger = true;
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
