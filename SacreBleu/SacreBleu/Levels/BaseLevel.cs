using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SacreBleu.GameObjects;

namespace SacreBleu.Levels
{
    class BaseLevel
    {
		//UI element references
        public PowerBar _powerBar;
        public Button _button;
		public DirectionGauge _directionGauge;

		//references to all the game objects in any given level

		public Frog _frog;
        public Obstacle[] _obstacles;
        public Hazard[] _hazards;

		//UI positions
		Vector2 directionGaugePosition,powerBarPosition,buttonPosition;

        public BaseLevel(int[,] levelLayout)
        {
			//declaring positions
			directionGaugePosition = new Vector2(75f, SacreBleuGame._instance._screenHeight - 50f);
			powerBarPosition = new Vector2(SacreBleuGame._instance._screenWidth - 75f, SacreBleuGame._instance._screenHeight - 75f);
			buttonPosition = new Vector2(SacreBleuGame._instance._screenWidth - 150f, SacreBleuGame._instance._screenHeight - 75f);

			//setting positions
			_directionGauge = new DirectionGauge(directionGaugePosition, SacreBleuGame._instance.arrowTexture);
            _powerBar = new PowerBar(powerBarPosition, SacreBleuGame._instance.basicSquare, SacreBleuGame._instance.basicSquare);
            _button = new Button(buttonPosition, SacreBleuGame._instance.basicSquare);

            GenerateLevel(levelLayout);
        }

        private void GenerateLevel(int[,] levelLayout)
        {
            //0 for background tile
            //1 for obstacle
            //2 for hazard
            //9 for frog
            List<Obstacle> tempObstacles = new List<Obstacle>();
            List<Hazard> tempHazards = new List<Hazard>();

            for(int x = 0; x < levelLayout.GetLength(1); x++)
            {
                for(int y = 0; y < levelLayout.GetLength(0); y++)
                {
                    //calculate tile position
                    Vector2 tilePosition = new Vector2(x * SacreBleuGame._instance._tileWidth, y * SacreBleuGame._instance._tileWidth);

                    //get the specified map component
                    int number = levelLayout[y, x];

                    if(number == 0)
                    {
                    }
                    else if(number == 1)
                    {
                        tempObstacles.Add(new Obstacle(tilePosition, SacreBleuGame._instance.basicSquare));
                    }
                    else if(number == 2)
                    {
                        tempHazards.Add(new Hazard(tilePosition, SacreBleuGame._instance.basicSquare));
                    }
                    else if(number == 9)
                    {
                        _frog = new Frog(tilePosition, SacreBleuGame._instance.basicSquare, 0.025f, 0.75f);
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
            foreach (Obstacle o in _obstacles)
            {
                if (o._bounds.Intersects(boundsToCheck))
                    return o;
            }
            foreach (Hazard h in _hazards)
            {
                if (h._bounds.Intersects(boundsToCheck))
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
                if (RectangleOverlapping(testBoundary) == null)
                    furthestFreePosition = positionToTry;
                else
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
            }
        }

        /*
        //major aspect of collision system
        //calculates nearest free space relative to object's position and velocity
        public Vector2 CalculateFreePosition(MoveableGameObject checkObject, Vector2 originalPosition, Vector2 destination, Rectangle boundingBox)
        {
            Vector2 movementToTry = destination - originalPosition;
            Vector2 furthestFreePosition = originalPosition;
            int movementStepCount = (int)(movementToTry.Length() * 2) + 1;
            Vector2 oneStep = movementToTry / movementStepCount;

            for(int i = 0; i <= movementStepCount; i++)
            {
                Vector2 positionToTry = originalPosition + oneStep * i;
                Rectangle testBoundary = CreateCollisionTestRectangle(positionToTry, boundingBox.Width, boundingBox.Height);
                GameObject collisionObject = RectangleOverlapping(testBoundary);
                if (collisionObject == null)
                    furthestFreePosition = positionToTry;
                else if (collisionObject._tag.Equals("Obstacle"))
                {
                    bool diagonalMove = movementToTry.X != 0 && movementToTry.Y != 0;
                    if (diagonalMove)
                    {
                        int stepsLeft = movementStepCount - (i - 1);

                        Vector2 remainingHorizontalMovement = oneStep.X * Vector2.UnitX * stepsLeft;
                        Vector2 finalHorizontalPosition = furthestFreePosition + remainingHorizontalMovement;
                        furthestFreePosition = CalculateFreePosition(checkObject, furthestFreePosition, finalHorizontalPosition, boundingBox);

                        Vector2 remainingVerticalMovement = oneStep.Y * Vector2.UnitY * stepsLeft;
                        Vector2 finalVerticalPosition = furthestFreePosition + remainingVerticalMovement;
                        furthestFreePosition = CalculateFreePosition(checkObject, furthestFreePosition, finalVerticalPosition, boundingBox);
                    }
                    break;
                }
                else if (collisionObject._tag.Equals("Hazard"))
                    checkObject.RegisterTriggerCollision(collisionObject);
            }

            return furthestFreePosition;
        }
        */

        //creates rectangle used for testing collisions
        private Rectangle CreateCollisionTestRectangle(Vector2 positionToTry, int width, int height)
        {
            return new Rectangle((int)positionToTry.X, (int)positionToTry.Y, width, height);
        }

        //draw all game objects
        public void Draw()
        {
            foreach (Obstacle o in _obstacles)
                o.Draw();
            foreach (Hazard h in _hazards)
                h.Draw();

            _frog.Draw();
            //_button.Draw();
            //_powerBar.Draw();
			//directionGauge.Draw();
        }
    }
}
