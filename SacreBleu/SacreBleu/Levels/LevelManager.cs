using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SacreBleu.Levels
{
    class LevelManager
    {
        public static LevelManager _instance;

        public List<BaseLevel> levels;
        private int levelIndex = 0;
        public BaseLevel currentLevel;

        public LevelManager()
        {
            _instance = this;

            levels = new List<BaseLevel>();

            levels.Add(new LevelOne());
            levels.Add(new ExampleLevel());

            currentLevel = levels[levelIndex];
        }

        public void Update(GameTime gameTime)
        {
            currentLevel.Update(gameTime);

            KeyboardState kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.Left) && levelIndex > 0)
            {
                levelIndex--;
                ChangeLevel(levels[levelIndex]);
            }
            else if(kstate.IsKeyDown(Keys.Right) && levelIndex < levels.Count - 1)
            {
                levelIndex++;
                ChangeLevel(levels[levelIndex]);
            }
        }

        private void ChangeLevel(BaseLevel newLevel)
        {
            currentLevel = newLevel;
        }
    }
}
