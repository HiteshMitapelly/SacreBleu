using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SacreBleu.GameObjects;

namespace SacreBleu.Levels
{
    class LevelTest
    {
        public static LevelTest instance { get; private set; }

        public BaseLevel baseLevel;

        public List<Obstacle> _obstacles;
        public List<Hazard> _hazards;

        public SpriteFont _levelFont;

        public LevelTest(SpriteBatch spriteBatch, Texture2D sprite, SpriteFont font)
        {
            instance = this;

            _obstacles = new List<Obstacle>();
            _obstacles.Add(new Obstacle(new Vector2(100, 100), spriteBatch, sprite));

            _hazards = new List<Hazard>();

            baseLevel = new BaseLevel(_obstacles.ToArray(), _hazards.ToArray());

            _levelFont = font;
        }

        public void Draw()
        {
            baseLevel.Draw();
        }
    }
}
