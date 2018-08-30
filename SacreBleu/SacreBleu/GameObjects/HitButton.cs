using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SacreBleu.Managers;

namespace SacreBleu.GameObjects
{
    class HitButton : ButtonGameObject
    {
        MouseState mouseState, oldMouseState;
        bool inRange;
        Vector2 mousePosition;

        public HitButton(Vector2 position, Texture2D sprite,string tag) : base(position, sprite,tag)
        {
            
            _tint = Color.Purple;
        }

        public override void Update(GameTime gameTime)
        {
            if (LevelManager._instance.currentLevel._frog._currentState != Frog.States.IDLE)
                return;

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
