using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SacreBleu.Managers;

namespace SacreBleu
{
    public class Camera
    {
        public static Camera _instance;
        public Matrix Transform { get; private set; }
        public Vector2 _position { get; set; }

        Viewport viewPort;
        float Ymovement;
        KeyboardState keyboardState, previousKeyboardState;
        Vector2 clampMaxforCamera, clampMinforCamera;
        public bool isViewingMap;


        public Camera(Viewport vp)
        {
            _instance = this;
            viewPort = vp;
            isViewingMap = false;
        }

        public void Update(Vector2 targetPosition)
        {
            Ymovement = 0f;
            keyboardState = Keyboard.GetState();
            isViewingMap = false;
            if (keyboardState.IsKeyDown(Keys.LeftControl))
            {
                isViewingMap = true;
                if (keyboardState.IsKeyDown(Keys.W))
                {

                    Ymovement = -10f;

                }

                if (keyboardState.IsKeyDown(Keys.S))
                {

                    Ymovement = 10f;

                }

            }
            if (isViewingMap)
            {
                Vector2 newCameraPosition = new Vector2(_position.X, _position.Y + Ymovement);
                _position = newCameraPosition;

            }
            if (!isViewingMap)
            {
                _position = new Vector2(targetPosition.X, targetPosition.Y);


            }

            clampMaxforCamera = new Vector2(viewPort.Width, LevelManager._instance.currentLevel.rowCount * SacreBleuGame._instance._tileWidth - (viewPort.Height / 2));

            clampMinforCamera = new Vector2(viewPort.Width, viewPort.Height / 2);

            _position = Vector2.Clamp(_position, clampMinforCamera, clampMaxforCamera);
            var translation = Matrix.CreateTranslation(
              -SacreBleuGame._instance._screenWidth / 2,
             -_position.Y,
              0);

            var offset = Matrix.CreateTranslation(
                viewPort.Bounds.Width / 2,
                viewPort.Bounds.Height / 2,
                0);

            Transform = Matrix.Lerp(Transform, translation * offset, 0.1f);


            previousKeyboardState = keyboardState;
        }


    }
}