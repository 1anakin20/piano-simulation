using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InteractivePiano.GameObject
{
    /// <summary>
    /// Virtual piano on screen
    /// </summary>
    public class Piano
    {
        private readonly PianoKeyColor[] _keyColorPattern =
        {
            PianoKeyColor.White,
            PianoKeyColor.Black,
            PianoKeyColor.White,
            PianoKeyColor.Black,
            PianoKeyColor.White,
            PianoKeyColor.White,
            PianoKeyColor.Black,
            PianoKeyColor.White,
            PianoKeyColor.Black,
            PianoKeyColor.White,
            PianoKeyColor.Black,
            PianoKeyColor.White
        };

        private readonly PianoKey[] _keys;
        private readonly int _numberOfKeys;

        /// <summary>
        /// A virtual piano to display on the screen
        /// </summary>
        /// <param name="spriteBatch">The <see cref="SpriteBatch"/> used in the game</param>
        /// <param name="whiteKeyTexture">The <see cref="Texture2D"/> texture of the white keys</param>
        /// <param name="blackKeyTexture">The <see cref="Texture2D"/> texture of the black key</param>
        /// <param name="startingPosition">The <see cref="Vector2"/> Position of the first key</param>
        /// <param name="numberKeys">The number of keys to display</param>
        /// <param name="startingKey">Where to start the pattern of the keys. <see cref="_keyColorPattern"/> and input the index of the start of the pattern</param>
        /// <param name="pressedColor">The <see cref="Color"/> of the key when they are pressed</param>
        public Piano(SpriteBatch spriteBatch, Texture2D whiteKeyTexture, Texture2D blackKeyTexture,
            Vector2 startingPosition, int numberKeys, int startingKey, Color pressedColor)
        {
            if (startingKey <= 0 && startingKey >= _keyColorPattern.Length)
                throw new ArgumentOutOfRangeException(nameof(startingKey));
            _numberOfKeys = numberKeys;
            // White keys need to be drawn first, so black keys can go on top of them
            _keys = new PianoKey[numberKeys];
            Vector2 currentWhiteKeyPosition = startingPosition;

            for (int i = 0; i < numberKeys; i++)
            {
                var keyColor = _keyColorPattern[(i + startingKey) % _keyColorPattern.Length];
                if (keyColor == PianoKeyColor.White)
                {
                    _keys[i] = new PianoKey(spriteBatch, whiteKeyTexture, currentWhiteKeyPosition, pressedColor);
                    currentWhiteKeyPosition += new Vector2(whiteKeyTexture.Width, 0);
                }
            }

            for (int i = 0; i < numberKeys; i++)
            {
                if (_keys[i] == null)
                {
                    var position = _keys[i - 1].Position +
                                   new Vector2(
                                       (int)Math.Ceiling(whiteKeyTexture.Width / 2d) + blackKeyTexture.Width / 2, 0);
                    _keys[i] = new PianoKey(spriteBatch, blackKeyTexture, position, pressedColor);
                }
            }
        }

        /// <summary>
        /// A virtual piano to display on the screen
        /// </summary>
        /// <param name="spriteBatch">The <see cref="SpriteBatch"/> used in the game</param>
        /// <param name="whiteKeyTexture">The <see cref="Texture2D"/> texture of the white keys</param>
        /// <param name="blackKeyTexture">The <see cref="Texture2D"/> texture of the black key</param>
        /// <param name="startingPosition">The <see cref="Vector2"/> Position of the first key</param>
        /// <param name="numberKeys">The number of keys to display</param>
        /// <param name="startingKey">Where to start the pattern of the keys. <see cref="_keyColorPattern"/> and input the index of the start of the pattern</param>
        public Piano(SpriteBatch spriteBatch, Texture2D whiteKeyTexture, Texture2D blackKeyTexture,
            Vector2 startingPosition, int numberKeys, int startingKey) : this(spriteBatch, whiteKeyTexture,
            blackKeyTexture, startingPosition, numberKeys, startingKey, Color.Red)
        {
        }

        /// <summary>
        /// Draws the keys on screen. Best called in the Draw method of the game.
        /// </summary>
        public void DrawKeys()
        {
            foreach (var key in _keys)
            {
                key.Draw();
            }
        }

        /// <summary>
        /// Presses the specified key
        /// </summary>
        /// <param name="key">The piano key number. They start at 0</param>
        public void PressKey(int key)
        {
            if (key < 0 || key >= _numberOfKeys) throw new ArgumentOutOfRangeException(nameof(key));
            _keys[key].PressKey();
        }

        /// <summary>
        /// Releases the specified key
        /// </summary>
        /// <param name="key">The piano key number. They start at 0</param>
        public void ReleaseKey(int key)
        {
            _keys[key].ReleaseKey();
        }
    }
}