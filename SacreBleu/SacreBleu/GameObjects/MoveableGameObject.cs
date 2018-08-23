using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SacreBleu.Levels;

namespace SacreBleu.GameObjects
{
    class MoveableGameObject : GameObject
    {
        public float _drag;
        public Vector2 _velocity;

        public MoveableGameObject(Vector2 position, Texture2D sprite, float drag)
            : base(position, sprite)
        {
            _drag = drag;
        }

        public virtual void Update(GameTime gameTime)
        {
            ApplyDrag();
            Move(gameTime);
        }

        private void Move(GameTime gameTime)
        {
            Vector2 previousPosition = _position;
            _position += _velocity * (float)gameTime.ElapsedGameTime.TotalMilliseconds / 15;

            _position = LevelTest.instance.baseLevel.CalculateFreePosition(this, previousPosition, _position, _bounds);
        }

        public void SetVelocity(Vector2 velocity)
        {
            _velocity = velocity;
        }

        private void ApplyDrag()
        {
            _velocity -= _velocity * _drag;
        }
    }
}
