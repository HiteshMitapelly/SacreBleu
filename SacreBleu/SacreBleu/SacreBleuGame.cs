using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SacreBleu.GameObjects;
using SacreBleu.Managers;

namespace SacreBleu
{
    public class SacreBleuGame : Game
    {
        //game instance
        public static SacreBleuGame _instance;

        //graphics
        public int _screenWidth, _screenHeight;
        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        public int _tileWidth = 32;
        public float baseScale = 0.0625f;

        //managers
        GameManager _gameManager;
        TileManager _tileManager;
        LevelManager _levelManager;

        //camera reference
        public Camera _camera;

        //textures
        public Texture2D basicSquare;
        public Texture2D frogTexture;
        public Texture2D arrowTexture;
        public Texture2D blackTileTexture;
        public Texture2D whiteTileTexture;
        public Texture2D counterTexture;
        public Texture2D burnerTexture;
        public Texture2D cuttingBoardTexture;
        public Texture2D goalTexture;
        public Texture2D fireTexture;
        public Texture2D powerBarTexture;

        //misc content
        public SpriteFont _levelFont;

        public SacreBleuGame()
        {
            if (_instance == null)
                _instance = this;

            _graphics = new GraphicsDeviceManager(this);

            _graphics.PreferredBackBufferWidth = 992;
            _graphics.PreferredBackBufferHeight = 668;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.IsMouseVisible = true;
            _screenHeight = _graphics.PreferredBackBufferHeight;
            _screenWidth = _graphics.PreferredBackBufferWidth;

            _camera = new Camera(GraphicsDevice.Viewport);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //load textures
            basicSquare = Content.Load<Texture2D>("Sprites/Props/white_tile");
            frogTexture = Content.Load<Texture2D>("Sprites/Frog/frog_idle_top");
            arrowTexture = Content.Load<Texture2D>("Sprites/texture");
            blackTileTexture = Content.Load<Texture2D>("Sprites/Props/black_tile");
            whiteTileTexture = Content.Load<Texture2D>("Sprites/Props/white_tile");
            counterTexture = Content.Load<Texture2D>("Sprites/Props/gray_tile");
            burnerTexture = Content.Load<Texture2D>("Sprites/Props/stove burn 1");
            cuttingBoardTexture = Content.Load<Texture2D>("Sprites/Props/cutting board 2");
            goalTexture = Content.Load<Texture2D>("Sprites/Props/pan");
            powerBarTexture = Content.Load<Texture2D>("Sprites/UI/PowerBar");

            //load fonts
            _levelFont = Content.Load<SpriteFont>("Fonts/Bebas");

            //create managers
            _gameManager = new GameManager();
            _tileManager = new TileManager();
            _levelManager = new LevelManager();
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {

                GameManager._instance._currentState = GameManager.GameStates.PAUSED;

            }

            // TODO: Add your update logic here
            _levelManager.Update(gameTime);
            if (GameManager._instance._currentState != GameManager.GameStates.IDLE)
                _camera.Update(_levelManager.currentLevel._frog._position);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);
            if (GameManager._instance._currentState == GameManager.GameStates.READY)
            {
                // TODO: Add your drawing code here						
                _spriteBatch.Begin(transformMatrix: _camera.Transform);
                _levelManager.currentLevel.Draw();
                base.Draw(gameTime);
                _spriteBatch.End();

                //Drawing UI sprites
                _spriteBatch.Begin();

                if (LevelManager._instance.currentLevel._frog._currentState == Frog.States.IDLE)
                {
                    LevelManager._instance.currentLevel._powerBar.Draw();
                    LevelManager._instance.currentLevel._hitbutton.Draw();
                }

                _spriteBatch.DrawString(_levelFont, "Hit Counter : " + LevelManager._instance.currentLevel.numberOfHits.ToString(), LevelManager._instance.currentLevel.counterPosition, Color.Black);
                _spriteBatch.End();
            }
            if (GameManager._instance._currentState == GameManager.GameStates.PAUSED)
            {
                _spriteBatch.Begin();

                _spriteBatch.Draw(LevelManager._instance.currentLevel.pauseMenuContinueButton._sprite, LevelManager._instance.currentLevel.pauseMenuContinueButton._position, Color.Orange);
                _spriteBatch.DrawString(_levelFont, "Continue", LevelManager._instance.currentLevel.pauseMenuContinueButton._position, Color.Black);

                _spriteBatch.Draw(LevelManager._instance.currentLevel.pauseMenuRestartButton._sprite, LevelManager._instance.currentLevel.pauseMenuRestartButton._position, Color.Green);
                _spriteBatch.DrawString(_levelFont, "Restart", LevelManager._instance.currentLevel.pauseMenuRestartButton._position, Color.Black);

                _spriteBatch.Draw(LevelManager._instance.currentLevel.quitButton._sprite, LevelManager._instance.currentLevel.quitButton._position, Color.Red);
                _spriteBatch.DrawString(_levelFont, "Quit", LevelManager._instance.currentLevel.quitButton._position, Color.Black);

                _spriteBatch.End();

            }
            if (GameManager._instance._currentState == GameManager.GameStates.WON)
            {
                _spriteBatch.Begin();
                _spriteBatch.DrawString(_levelFont, "YOU WON", new Vector2(_screenWidth / 2, _screenHeight / 2 - 400f), Color.Yellow);

                _spriteBatch.Draw(LevelManager._instance.currentLevel.wonScreenNextLevelButton._sprite, LevelManager._instance.currentLevel.wonScreenNextLevelButton._position, Color.Orange);
                _spriteBatch.DrawString(_levelFont, "Next Level", LevelManager._instance.currentLevel.pauseMenuContinueButton._position, Color.Black);

                _spriteBatch.Draw(LevelManager._instance.currentLevel.wonScreenRetryLevelButton._sprite, LevelManager._instance.currentLevel.wonScreenRetryLevelButton._position, Color.Green);
                _spriteBatch.DrawString(_levelFont, "Retry Level", LevelManager._instance.currentLevel.pauseMenuRestartButton._position, Color.Black);

                _spriteBatch.Draw(LevelManager._instance.currentLevel.quitButton._sprite, LevelManager._instance.currentLevel.quitButton._position, Color.Red);
                _spriteBatch.DrawString(_levelFont, "Quit", LevelManager._instance.currentLevel.quitButton._position, Color.Black);


                _spriteBatch.End();

            }
            if (GameManager._instance._currentState == GameManager.GameStates.IDLE)
            {
                _spriteBatch.Begin();
                LevelManager._instance.titleScreen.Draw();
                _spriteBatch.End();
            }
        }
    }
}
