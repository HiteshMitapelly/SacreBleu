using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SacreBleu.GameObjects;
using SacreBleu.Managers;

namespace SacreBleu.Levels
{
    class TitleScreen : BaseLevel
    {
        public ButtonGameObject startGameButton;

        public Vector2 startGameButtonPosition;
        public TitleScreen() : base()
        {
            startGameButtonPosition = new Vector2(SacreBleuGame._instance._screenWidth / 2, SacreBleuGame._instance._screenHeight / 2);
            startGameButton = new ButtonGameObject(startGameButtonPosition, SacreBleuGame._instance.basicSquare, "startGameButton");
        }
        public override void Update(GameTime gameTime)
        {
            startGameButton.Update(gameTime);
            if (startGameButton.CurrentButtonState() == ButtonState.Pressed)
            {
                LevelManager._instance.GoToFirstLevel();
                GameManager._instance._currentState = GameManager.GameStates.READY;
            }

        }
        public override void Draw()
        {
            //base.Draw();
            SacreBleuGame._instance._spriteBatch.Draw(startGameButton._sprite, startGameButtonPosition, Color.White);
            SacreBleuGame._instance._spriteBatch.DrawString(SacreBleuGame._instance._levelFont, "Start Game", new Vector2(startGameButtonPosition.X, startGameButtonPosition.Y + 32), Color.Black);
        }
    }
}
