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
        public  int _screenWidth, _screenHeight;
        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        public int _tileWidth = 32;
        public float _scaleMultiplier = 0.0625f;

        //managers
        GameManager _gameManager;
        LevelManager _levelManager;

        //camera reference
        public Camera _camera;

        //textures
        public Texture2D frogTexture;
		public Texture2D arrowTexture;
        public Texture2D basicSquare;

        //misc content
        public SpriteFont _levelFont;
			
		public SacreBleuGame()
        {
            if(_instance == null)
                _instance = this;

            _graphics = new GraphicsDeviceManager(this);

            _graphics.PreferredBackBufferWidth = 960;
            _graphics.PreferredBackBufferHeight = 668;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
			// TODO: Add your initialization logic here
			this.IsMouseVisible = true;
			_screenHeight = _graphics.PreferredBackBufferHeight;
			_screenWidth = _graphics.PreferredBackBufferWidth;

            //create managers
            _gameManager = new GameManager();
            _levelManager = new LevelManager();

            _camera = new Camera(GraphicsDevice.Viewport);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            //load textures
			frogTexture = Content.Load<Texture2D>("Sprites/Frog/frog_idle_top");
            basicSquare = Content.Load<Texture2D>("Sprites/BasicSquare");
			arrowTexture = Content.Load<Texture2D>("Sprites/texture");

            //load fonts
            _levelFont = Content.Load<SpriteFont>("Fonts/Bebas");
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            _levelManager.Update(gameTime);
            _camera.Update(_levelManager.currentLevel._frog._position);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);

			// TODO: Add your drawing code here						
			_spriteBatch.Begin(transformMatrix: _camera.Transform);
            _levelManager.currentLevel.Draw();
            base.Draw(gameTime);
            _spriteBatch.End();

			//Drawing UI sprites
			_spriteBatch.Begin();
            LevelManager._instance.currentLevel._powerBar.Draw();
            LevelManager._instance.currentLevel._button.Draw();
			_spriteBatch.End();

		}
    }
}
