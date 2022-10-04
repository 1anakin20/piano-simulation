using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InteractivePiano.GameObject
{
    public class PianoKey
    {
        private readonly Texture2D _sprite;
        public Vector2 Position { get; }
        private readonly Color _pressedColor;
        private readonly SpriteBatch _spriteBatch;
        private bool _isPressed;

        public PianoKey(SpriteBatch spriteBatch, Texture2D sprite, Vector2 position, Color pressedColor)
        {
            _spriteBatch = spriteBatch;
            _sprite = sprite;
            Position = position;
            _pressedColor = pressedColor;
            _isPressed = false;
        }

        public void Draw()
        {
            if (_isPressed)
            {
                _spriteBatch.Draw(_sprite, Position, _pressedColor);
            }
            else
            {
                _spriteBatch.Draw(_sprite, Position, Color.White);
            }
        }

        public void PressKey()
        {
            _isPressed = true;
        }

        public void ReleaseKey()
        {
            _isPressed = false;
        }
    }
}