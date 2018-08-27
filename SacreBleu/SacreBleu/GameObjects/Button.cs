using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SacreBleu.Managers;

namespace SacreBleu.GameObjects
{
    class Button : GameObject
    {
        MouseState mouseState, oldMouseState;
        bool inRange;
        Vector2 mousePosition;

        public Button(Vector2 position, Texture2D sprite) : base(position, sprite)
        {
            _tag = "button";
            _tint = Color.Red;
        }

        public void Update(GameTime gameTime)
        {
            mousePosition = new Vector2(mouseState.X, mouseState.Y);

            mouseState = Mouse.GetState();

            inRange = GetBounds().Contains(new Point((int)mousePosition.X, (int)mousePosition.Y));

            if (inRange)
            {

                if ((mouseState.LeftButton == ButtonState.Pressed) && (oldMouseState.LeftButton == ButtonState.Pressed))
                {

                    LevelManager._instance.currentLevel._powerBar.startBar = true;
                }

                if ((mouseState.LeftButton == ButtonState.Released) && (oldMouseState.LeftButton == ButtonState.Pressed))
                {
                    LevelManager._instance.currentLevel._powerBar.startBar = false;
                    LevelManager._instance.currentLevel._powerBar.setpower();
                }
            }

            oldMouseState = mouseState;
        }
        public override void Draw()
        {
            base.Draw();
        }
    }
}
