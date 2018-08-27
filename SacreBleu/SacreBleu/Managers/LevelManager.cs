using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SacreBleu.Levels;
using System.Collections.Generic;

namespace SacreBleu.Managers
{
    class LevelManager
    {
        public static LevelManager _instance;

        public List<BaseLevel> levels;
        private int levelIndex;
        public BaseLevel currentLevel;

        public LevelManager()
        {
            _instance = this;

            levels = new List<BaseLevel>
            {
                new LevelOne(),
                new LevelTwo(),
                new LevelThree()
            };

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
            else if (kstate.IsKeyDown(Keys.Right))
                GoToNextLevel();
        }

        private void ChangeLevel(BaseLevel newLevel)
        {
            currentLevel = newLevel;
        }

        public void GoToNextLevel()
        {
            if (levelIndex < levels.Count - 1)
            {
                levelIndex++;
                ChangeLevel(levels[levelIndex]);
            }
        }
    }
}
