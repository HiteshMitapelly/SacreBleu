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
        GameTime _gameTime;

        public float _drag;
        public float _bounce;
        public Vector2 _velocity;

        public MoveableGameObject(Vector2 position, Texture2D sprite, float drag, float bounce)
            : base(position, sprite)
        {
            _drag = drag;
            _bounce = bounce;
        }

        public virtual void Update(GameTime gameTime)
        {
            _gameTime = gameTime;
            ApplyDrag();
            Move();
        }

        private void Move()
        {
            Vector2 previousPosition = _position;
            AddVelocity();

            LevelTest.instance.baseLevel.CollisionCheck(this, previousPosition, _position, _bounds);
            //_position = LevelTest.instance.baseLevel.CalculateFreePosition(this, previousPosition, _position, _bounds);
        }

        public void AddVelocity()
        {
            _position += _velocity * (float)_gameTime.ElapsedGameTime.TotalMilliseconds / 15;
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
