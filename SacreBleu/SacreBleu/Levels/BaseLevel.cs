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
        public Obstacle[] _obstacles;
        public Hazard[] _hazards;
        public GameObject _gameObjects;

        public BaseLevel(Obstacle[] obstacles, Hazard[] hazards)
        {
            _obstacles = obstacles;
            _hazards = hazards;
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

        public void Draw()
        {
            foreach (Obstacle o in _obstacles)
                o.Draw();
            foreach (Hazard h in _hazards)
                h.Draw();
        }
    }
}
