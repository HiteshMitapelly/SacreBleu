using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SacreBleu.Managers;

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

            LevelManager._instance.currentLevel.CollisionCheck(this, previousPosition, _position, _bounds);
            //_position = LevelTest.instance.baseLevel.CalculateFreePosition(this, previousPosition, _position, _bounds);
        }

        public void AddVelocity()
        {
            _position += _velocity * (float)_gameTime.ElapsedGameTime.TotalMilliseconds / 15;

            if (_velocity.Length() < 0.1f)
                _velocity = Vector2.Zero;
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
