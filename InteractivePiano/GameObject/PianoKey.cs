using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InteractivePiano.GameObject
{
    /// <summary>
    /// A graphical piano key
    /// </summary>
    public class PianoKey
    {
        private readonly Texture2D _sprite;

        /// <summary>
        /// The position of the key
        /// </summary>
        public Vector2 Position { get; }

        private readonly Color _pressedColor;
        private readonly SpriteBatch _spriteBatch;
        private bool _isPressed;

        /// <summary>
        /// A graphical piano key
        /// </summary>
        /// <param name="spriteBatch">The <see cref="SpriteBatch"/> used in the game</param>
        /// <param name="sprite">The <see cref="Texture2D"/> the sprite of the key</param>
        /// <param name="position"> the <see cref="Vector2"/> position of the key</param>
        /// <param name="pressedColor"> The <see cref="Color"/> of the key when pressed</param>
        public PianoKey(SpriteBatch spriteBatch, Texture2D sprite, Vector2 position, Color pressedColor)
        {
            _spriteBatch = spriteBatch;
            _sprite = sprite;
            Position = position;
            _pressedColor = pressedColor;
            _isPressed = false;
        }

        /// <summary>
        /// Draws the key at its position with its corresponding color if pressed or not
        /// </summary>
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

        /// <summary>
        /// Presses the key, changing its color
        /// </summary>
        public void PressKey()
        {
            _isPressed = true;
        }

        /// <summary>
        /// Releases the key, changing its color
        /// </summary>
        public void ReleaseKey()
        {
            _isPressed = false;
        }
    }
}