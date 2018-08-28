using Microsoft.Xna.Framework;
using SacreBleu.GameObjects;
using SacreBleu.Managers;
using System.Collections.Generic;

namespace SacreBleu.Levels
{
    class BaseLevel
    {
        //UI element references
        public PowerBar _powerBar;
        public Button _button;
        public DirectionGauge _directionGauge;

        //references to all the game objects in any given level
        public int[,] levelLayout;
        public Frog _frog;
        private Vector2 _frogStartingPosition;
        public Goal _goal;
        public Obstacle[] _obstacles;
        public Hazard[] _hazards;

        //UI positions
        Vector2 directionGaugePosition, powerBarPosition, buttonPosition;

        public BaseLevel()
        {
            //declaring positions
            directionGaugePosition = new Vector2(125f, SacreBleuGame._instance._screenHeight - 50f);
            powerBarPosition = new Vector2(SacreBleuGame._instance._screenWidth - 75f, SacreBleuGame._instance._screenHeight - 75f);
            buttonPosition = new Vector2(SacreBleuGame._instance._screenWidth - 150f, SacreBleuGame._instance._screenHeight - 75f);

            //setting positions
            _directionGauge = new DirectionGauge(directionGaugePosition, SacreBleuGame._instance.arrowTexture);
            _powerBar = new PowerBar(powerBarPosition, SacreBleuGame._instance.powerBarTexture, SacreBleuGame._instance.basicSquare);
            _button = new Button(buttonPosition, SacreBleuGame._instance.basicSquare);

            //GenerateLevel(levelLayout);
        }

        protected void GenerateLevel(int[,] layout)
        {
            levelLayout = layout;

            //0 for background tile
            //1 for obstacle
            //2 for hazard
            //6 for goal
            //9 for frog
            List<Obstacle> tempObstacles = new List<Obstacle>();
            List<Hazard> tempHazards = new List<Hazard>();

            for (int x = 0; x < levelLayout.GetLength(1); x++)
            {
                for (int y = 0; y < levelLayout.GetLength(0); y++)
                {
                    //calculate tile position
                    Vector2 tilePosition = new Vector2(x * SacreBleuGame._instance._tileWidth, y * SacreBleuGame._instance._tileWidth);

                    //get the specified map component
                    int number = levelLayout[y, x];

                    if (number == 0)
                    {
                    }
                    else if (number == 1)
                    {
                        tempObstacles.Add(new Obstacle(tilePosition, SacreBleuGame._instance.basicSquare));
                    }
                    else if (number == 2)
                    {
                        tempHazards.Add(new Hazard(tilePosition, SacreBleuGame._instance.fireTexture));
                    }
                    else if (number == 6)
                    {
                        _goal = new Goal(tilePosition, SacreBleuGame._instance.goalTexture);
                    }
                    else if (number == 9)
                    {
                        _frogStartingPosition = tilePosition;
                        _frog = new Frog(_frogStartingPosition, SacreBleuGame._instance.frogTexture, 0.015f, 0.75f);
                    }
                }
            }

            _obstacles = tempObstacles.ToArray();
            _hazards = tempHazards.ToArray();
        }

        //update game objects and camera
        public void Update(GameTime gameTime)
        {
            _frog.Update(gameTime);

            _powerBar.Update(gameTime);
            _button.Update(gameTime);
            _directionGauge.Update(gameTime);

        }

        //check for overlapping rectangles
        public GameObject RectangleOverlapping(Rectangle boundsToCheck)
        {
            if (_goal != null && _goal.GetBounds().Intersects(boundsToCheck))
                return _goal;

            foreach (Obstacle o in _obstacles)
            {
                if (o.GetBounds().Intersects(boundsToCheck))
                    return o;
            }
            foreach (Hazard h in _hazards)
            {
                if (h.GetBounds().Intersects(boundsToCheck))
                    return h;
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
                    RestartLevel();
                }
                else if (_goal != null && objectOverlapping._tag.Equals("Goal"))
                {
                    if (_frog._velocity.Length() <= 0f)
                        LevelManager._instance.GoToNextLevel();
                }
            }
        }

        //creates rectangle used for testing collisions
        private Rectangle CreateCollisionTestRectangle(Vector2 positionToTry, int width, int height)
        {
            return new Rectangle((int)positionToTry.X, (int)positionToTry.Y, width, height);
        }

        private void RestartLevel()
        {
            _frog.Death(_frogStartingPosition);
            _directionGauge._angle = 0f;
        }

        //draw all game objects
        public void Draw()
        {
            foreach (Obstacle o in _obstacles)
                o.Draw();
            foreach (Hazard h in _hazards)
                h.Draw();

            if (_goal != null)
                _goal.Draw();
            if (_frog._currentState == Frog.States.IDLE)
            {
                _directionGauge.Draw();
            }

            _frog.Draw();

            //_button.Draw();
            //_powerBar.Draw();
        }
    }
}
