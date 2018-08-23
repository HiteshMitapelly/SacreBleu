using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SacreBleu.GameObjects;
using SacreBleu.Levels;

namespace SacreBleu
{
	public enum Gamestates { PAUSED, READY, RELEASED}
    public class SacreBleuGame : Game
    {
        public static SacreBleuGame _instance;

		public  int _screenWidth, _screenHeight;
        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;

        //texture references
        public Texture2D frogTexture;

        //testing level design
        public Texture2D basicSquare;
        public SpriteFont _levelFont;
        LevelTest levelTest;

        public Gamestates currentState;
			
		public SacreBleuGame()
        {
            if(_instance == null)
                _instance = this;

            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
			// TODO: Add your initialization logic here
			this.IsMouseVisible = true;
			_screenHeight = _graphics.PreferredBackBufferHeight;
			_screenWidth = _graphics.PreferredBackBufferWidth;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

			// TODO: use this.Content to load your game content here
			currentState = Gamestates.READY;
            
            //load textures
			frogTexture = Content.Load<Texture2D>("ball");
            basicSquare = Content.Load<Texture2D>("Sprites/BasicSquare");

            //load fonts
            _levelFont = Content.Load<SpriteFont>("Fonts/Bebas");

            //create level
            levelTest = new LevelTest(basicSquare, _levelFont);
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
            levelTest.baseLevel.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            levelTest.Draw();

            base.Draw(gameTime);
        }
    }
}
