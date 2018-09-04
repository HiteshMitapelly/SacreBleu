using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SacreBleu.GameObjects;
using SacreBleu.Managers;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace SacreBleu.Levels
{
    class BaseLevel
    {
        //par and current hitcounter
        public int numberOfHits;
        public int par;

        //UI element references
        public PowerBar _powerBar;
        public HitButton _hitbutton;
        public DirectionGauge _directionGauge;
        public ButtonGameObject pauseMenuContinueButton, pauseMenuRestartButton, quitButton, wonScreenNextLevelButton, wonScreenRetryLevelButton;

        //references to all the game objects in any given level
        public Frog _frog;
        public Vector2 _frogStartingPosition;
        public Goal _goal;
        public List<GameObject> entities;
        public List<GameObject> bgEntities;

        //UI positions
        public Vector2 counterPosition, pauseMenuContinuePosition, pauseMenuRestartPosition, quitPosition, wonScreenNextLevelPosition, wonScreenRetryLevelPosition;

        Vector2 directionGaugePosition, powerBarPosition, buttonPosition;

        public int rowCount;

        public BaseLevel()
        {
            //declaring positions
            directionGaugePosition = new Vector2(125f, SacreBleuGame._instance._screenHeight - 50f);
            powerBarPosition = new Vector2(SacreBleuGame._instance._screenWidth - 75f, SacreBleuGame._instance._screenHeight - 75f);
            buttonPosition = new Vector2(SacreBleuGame._instance._screenWidth - 150f, SacreBleuGame._instance._screenHeight - 75f);
            counterPosition = new Vector2(75f, SacreBleuGame._instance._screenHeight / 2 - (0.90f * SacreBleuGame._instance._screenHeight / 2));
            pauseMenuContinuePosition = new Vector2(SacreBleuGame._instance._screenWidth / 2, SacreBleuGame._instance._screenHeight / 2 - 50f);
            pauseMenuRestartPosition = new Vector2(SacreBleuGame._instance._screenWidth / 2, SacreBleuGame._instance._screenHeight / 2);
            quitPosition = new Vector2(SacreBleuGame._instance._screenWidth / 2, SacreBleuGame._instance._screenHeight / 2 + 50f);
            wonScreenNextLevelPosition = new Vector2(SacreBleuGame._instance._screenWidth / 2, SacreBleuGame._instance._screenHeight / 2 - 50f);
            wonScreenRetryLevelPosition = new Vector2(SacreBleuGame._instance._screenWidth / 2, SacreBleuGame._instance._screenHeight / 2);

            //setting positions
            _directionGauge = new DirectionGauge(directionGaugePosition, SacreBleuGame._instance.arrowTexture);
            _powerBar = new PowerBar(powerBarPosition, SacreBleuGame._instance.powerBarTexture, SacreBleuGame._instance.basicSquare);
            _hitbutton = new HitButton(buttonPosition, SacreBleuGame._instance.basicSquare, "hitButton");
            pauseMenuContinueButton = new ButtonGameObject(pauseMenuContinuePosition, SacreBleuGame._instance.basicSquare, "continueButton");
            pauseMenuRestartButton = new ButtonGameObject(pauseMenuRestartPosition, SacreBleuGame._instance.basicSquare, "restartButton");
            quitButton = new ButtonGameObject(quitPosition, SacreBleuGame._instance.basicSquare, "quitButton");
            wonScreenNextLevelButton = new ButtonGameObject(wonScreenNextLevelPosition, SacreBleuGame._instance.basicSquare, "nextLevelButton");
            wonScreenRetryLevelButton = new ButtonGameObject(wonScreenRetryLevelPosition, SacreBleuGame._instance.basicSquare, "retryLevelButton");

            //GenerateLevel(levelLayout);

            numberOfHits = 0;
            par = 0;

            entities = new List<GameObject>();
            bgEntities = new List<GameObject>();
        }

        protected void CreateBackgroundLayout(string filePath)
        {
            List<List<int>> layout = new List<List<int>>();
            using (StreamReader reader = File.OpenText(filePath))
            {
                string currentLine;
                while ((currentLine = reader.ReadLine()) != null)
                {
                    List<int> currentRow = new List<int>();
                    string[] columns = currentLine.Split(',');
                    foreach (string cell in columns)
                    {
                        int tile = int.Parse(cell.Trim());
                        currentRow.Add(tile);
                    }

                    layout.Add(currentRow);
                }
            }

            GenerateLevel(layout);
        }

        protected void CreateLevelLayout(string filePath)
        {
            List<List<int>> layout = new List<List<int>>();
            using (StreamReader reader = File.OpenText(filePath))
            {
                string currentLine;
                while ((currentLine = reader.ReadLine()) != null)
                {
                    List<int> currentRow = new List<int>();
                    string[] columns = currentLine.Split(',');
                    foreach (string cell in columns)
                    {
                        int tile = int.Parse(cell.Trim());
                        currentRow.Add(tile);
                    }

                    layout.Add(currentRow);
                }
            }

            GenerateLevel(layout);
        }

        protected void GenerateLevel(List<List<int>> layout)
        {
            //0 = burner
            //19 = butter_before
            //3 = counter_dark
            //4 = counter_light
            //20 = cutting_board
            //23 = flour
            //21 = garnished_plate
            //22 = pan
            //24 = sink
            //10 = black_tile
            //11 = white_tile
            rowCount = layout.Count;
            for (int i = 0; i < layout.Count; i++)
            {
                List<int> currentRow = layout[i];
                for (int j = 0; j < currentRow.Count; j++)
                {
                    Vector2 tilePosition = new Vector2(j * SacreBleuGame._instance._tileWidth, i * SacreBleuGame._instance._tileWidth);

                    entities.Add(TileManager.Instance.CreateTile(currentRow[j], tilePosition));
                }
            }

            foreach (GameObject g in entities)
            {
                if (g != null && g._tag.Equals("Cutting Board"))
                {
                    _frogStartingPosition = g._position;
                    _frog = new Frog(new Vector2(g._position.X + 64, g._position.Y + 28), SacreBleuGame._instance.frogTexture, 0.015f, 0.75f);
                }
                else if (g != null && g._tag.Equals("Goal"))
                    _goal = (Goal)g;
            }
        }

        protected void GenerateBackground(List<List<int>> layout)
        {
            rowCount = layout.Count;
            for (int i = 0; i < layout.Count; i++)
            {
                List<int> currentRow = layout[i];
                for (int j = 0; j < currentRow.Count; j++)
                {
                    Vector2 tilePosition = new Vector2(j * SacreBleuGame._instance._tileWidth, i * SacreBleuGame._instance._tileWidth);

                    bgEntities.Add(TileManager.Instance.CreateTile(currentRow[j], tilePosition));
                }
            }
        }

        //update game objects and camera
        public virtual void Update(GameTime gameTime)
        {
            //UI Updates
            pauseMenuContinueButton.Update(gameTime);
            pauseMenuRestartButton.Update(gameTime);
            quitButton.Update(gameTime);
            wonScreenRetryLevelButton.Update(gameTime);
            wonScreenNextLevelButton.Update(gameTime);

            //Playing state
            if (GameManager._instance._currentState == GameManager.GameStates.READY)
            {
                _frog.Update(gameTime);

                _powerBar.Update(gameTime);
                _hitbutton.Update(gameTime);
                _directionGauge.Update(gameTime);
            }

            //Paused state
            if (GameManager._instance._currentState == GameManager.GameStates.PAUSED)
            {
                if (pauseMenuContinueButton.CurrentButtonState() == ButtonState.Pressed)
                {
                    GameManager._instance._currentState = GameManager.GameStates.READY;
                }
                if (pauseMenuRestartButton.CurrentButtonState() == ButtonState.Pressed)
                {

                    GameManager._instance._currentState = GameManager.GameStates.READY;
                    numberOfHits = 0;
                    ResetFrog();
                }
                if (quitButton.CurrentButtonState() == ButtonState.Pressed)
                {
                    SacreBleuGame._instance.Exit();
                }
            }

            //Won state
            if (GameManager._instance._currentState == GameManager.GameStates.WON)
            {
                if (wonScreenNextLevelButton.CurrentButtonState() == ButtonState.Pressed)
                {
                    LevelManager._instance.GoToNextLevel();
                    GameManager._instance._currentState = GameManager.GameStates.READY;
                }
                if (wonScreenRetryLevelButton.CurrentButtonState() == ButtonState.Pressed)
                {

                    GameManager._instance._currentState = GameManager.GameStates.READY;
                    numberOfHits = 0;
                    ResetFrog();
                }
                if (quitButton.CurrentButtonState() == ButtonState.Pressed)
                {
                    SacreBleuGame._instance.Exit();
                }
            }

        }

        //check for overlapping rectangles
        public GameObject RectangleOverlapping(Rectangle boundsToCheck)
        {
            if (_goal != null && _goal.GetBounds().Intersects(boundsToCheck))
                return _goal;

            foreach (GameObject g in entities)
            {
                if (g != null && (g.GetBounds().Intersects(boundsToCheck)))
                {
                    if (g._tag.Equals("Obstacle") || g._tag.Equals("Hazard"))
                        return g;
                    else if (g._tag.Equals("Butter"))
                    {
                        Debug.WriteLine("butter");
                        _frog.buttered = true;
                        _frog.floured = false;
                    }
                    else if (g._tag.Equals("Flour"))
                    {
                        Debug.WriteLine("flour");
                        _frog.buttered = false;
                        _frog.floured = true;
                    }
                }
            }
            foreach (GameObject g in bgEntities)
            {
                if (g != null && (g._tag.Equals("Obstacle") || g._tag.Equals("Hazard")))
                {
                    if (g.GetBounds().Intersects(boundsToCheck))
                        return g;
                }
            }

            return null;
        }

        public void CollisionCheck(MoveableGameObject objectToCheck, Vector2 originalPosition, Vector2 destination, Rectangle boundingRectangle)
        {
            Vector2 movementToTry = destination - originalPosition;
            Vector2 furthestFreePosition = originalPosition;
            int movementStepCount = (int)(movementToTry.Length() * 2) + 1;
            Vector2 oneStep = movementToTry / movementStepCount;

            for (int i = 1; i <= movementStepCount; i++)
            {
                Vector2 positionToTry = originalPosition + oneStep * i;
                Rectangle testBoundary =
                    CreateCollisionTestRectangle(positionToTry, boundingRectangle.Width, boundingRectangle.Height);

                GameObject objectOverlapping = RectangleOverlapping(testBoundary);

                if (objectOverlapping == null)
                    furthestFreePosition = positionToTry;
                else if (objectOverlapping._tag.Equals("Obstacle"))
                {
                    objectToCheck._position = furthestFreePosition;

                    Rectangle collisionTestX = CreateCollisionTestRectangle(new Vector2(objectToCheck._position.X - 1, objectToCheck._position.Y), boundingRectangle.Width + 2, boundingRectangle.Height);
                    Rectangle collisionTestY = CreateCollisionTestRectangle(new Vector2(objectToCheck._position.X, objectToCheck._position.Y - 1), boundingRectangle.Width, boundingRectangle.Height + 2);

                    //bounce!
                    if (RectangleOverlapping(collisionTestX) != null)
                    {
                        objectToCheck._velocity.X *= -1 * _frog._bounce;
                        objectToCheck._velocity.Y *= _frog._bounce;
                    }
                    if (RectangleOverlapping(collisionTestY) != null)
                    {
                        objectToCheck._velocity.Y *= -1 * _frog._bounce;
                        objectToCheck._velocity.X *= _frog._bounce;
                    }

                    objectToCheck.AddVelocity();

                    break;
                }
                else if (objectOverlapping._tag.Equals("Hazard"))
                {
                    ResetFrog();
                }
                else if (_goal != null && objectOverlapping._tag.Equals("Goal"))
                {
                    if (_frog._velocity.Length() <= 0f)
                    {
                        GameManager._instance._currentState = GameManager.GameStates.WON;
                    }
                }
            }
        }

        //creates rectangle used for testing collisions
        private Rectangle CreateCollisionTestRectangle(Vector2 positionToTry, int width, int height)
        {
            return new Rectangle((int)positionToTry.X, (int)positionToTry.Y, width, height);
        }

        private void ResetFrog()
        {
            _frog.Death();
            _directionGauge._angle = 0f;
        }

        //draw all game objects
        public virtual void Draw()
        {
            if (GameManager._instance._currentState == GameManager.GameStates.READY)
            {
                foreach (GameObject g in entities)
                {
                    if (g != null)
                        g.Draw();
                }
                foreach (GameObject g in bgEntities)
                {
                    if (g != null)
                        g.Draw();
                }

                if (_frog._currentState == Frog.States.IDLE)
                {
                    _directionGauge.Draw();
                }

                _frog.Draw();
            }
        }
    }
}
