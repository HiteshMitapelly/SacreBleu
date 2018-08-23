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
        Camera camera;

        public Frog _frog;
        public Obstacle[] _obstacles;
        public Hazard[] _hazards;

        public BaseLevel(Vector2 frogStartingPosition, Obstacle[] obstacles, Hazard[] hazards)
        {
            camera = new Camera();

            _frog = new Frog(frogStartingPosition, SacreBleuGame._instance.frogTexture, 0.1f);
            _obstacles = obstacles;
            _hazards = hazards;
        }

        public void Update(GameTime gameTime)
        {
            _frog.Update(gameTime);
            camera.Follow(_frog);
        }

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

        public Vector2 CalculateFreePosition(GameObject checkObject, Vector2 originalPosition, Vector2 destination, Rectangle boundingBox)
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

        private Rectangle CreateCollisionTestRectangle(Vector2 positionToTry, int width, int height)
        {
            return new Rectangle((int)positionToTry.X, (int)positionToTry.Y, width, height);
        }

        public void Draw()
        {
            SacreBleuGame._instance._spriteBatch.Begin(transformMatrix: camera.Transform);

            foreach (Obstacle o in _obstacles)
                o.Draw();
            foreach (Hazard h in _hazards)
                h.Draw();

            _frog.Draw();

            SacreBleuGame._instance._spriteBatch.End();
        }
    }
}
