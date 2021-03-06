﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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
        public Texture2D titleTexture;

        public Texture2D basicSquare;
        public Texture2D frogTexture;
        public Texture2D arrowTexture;
        public Texture2D powerBarTexture;

        public Texture2D butterBeforeTexture;
        public Texture2D butterAfterTexture;
        public Texture2D sinkTexture;
        public Texture2D blackTileTexture;
        public Texture2D smallBlackTileTexture;
        public Texture2D smallWhiteTileTexture;
        public Texture2D whiteTileTexture;
        public Texture2D counterDarkTexture;
        public Texture2D counterLightTexture;
        public Texture2D burnerTexture;
        public Texture2D cuttingBoardTexture;
        public Texture2D goalTexture;
        public Texture2D fireTexture;
        public Texture2D flourTexture;

        //misc content
        public SpriteFont _levelFont;
        private Song song;

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
            titleTexture = Content.Load<Texture2D>("Sprites/UI/Open Scene w_UI");

            basicSquare = Content.Load<Texture2D>("Sprites/BasicSquare");
            frogTexture = Content.Load<Texture2D>("Sprites/Frog/frog_idle_top");
            arrowTexture = Content.Load<Texture2D>("Sprites/texture");
            powerBarTexture = Content.Load<Texture2D>("Sprites/UI/PowerBar");

            butterBeforeTexture = Content.Load<Texture2D>("Sprites/Props/butter_before");
            butterAfterTexture = Content.Load<Texture2D>("Sprites/Props/butter_after");
            sinkTexture = Content.Load<Texture2D>("Sprites/Props/sink");
            blackTileTexture = Content.Load<Texture2D>("Sprites/Props/tile_black");
            smallBlackTileTexture = Content.Load<Texture2D>("Sprites/Props/tile_blackSmall");
            smallWhiteTileTexture = Content.Load<Texture2D>("Sprites/Props/tile_whiteSmall");
            whiteTileTexture = Content.Load<Texture2D>("Sprites/Props/tile_white");
            counterDarkTexture = Content.Load<Texture2D>("Sprites/Props/counter_dark");
            counterLightTexture = Content.Load<Texture2D>("Sprites/Props/counter_light");
            burnerTexture = Content.Load<Texture2D>("Sprites/Props/burner");
            cuttingBoardTexture = Content.Load<Texture2D>("Sprites/Props/cuttingBoard");
            goalTexture = Content.Load<Texture2D>("Sprites/Props/pan");
            fireTexture = Content.Load<Texture2D>("Sprites/Props/flame");
            flourTexture = Content.Load<Texture2D>("Sprites/Props/flour");

            //load fonts
            _levelFont = Content.Load<SpriteFont>("Fonts/Bebas");

            //audio
            song = Content.Load<Song>("Audio/Bushwick Tarantella Loop");
            MediaPlayer.Play(song);

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
